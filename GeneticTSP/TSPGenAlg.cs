using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticTSP
{
    public class RndGen : IRndGen
    {
        private static Random RNG = new Random(DateTime.Now.Millisecond);

        public Tuple<int, int> ChooseSpan(int min_span, int max_span)
        {
            int start = RNG.Next(0, max_span - min_span);
            int end = RNG.Next(start + 1, max_span);
            return Tuple.Create(start, end);
        }

        public int Next()
        {
            return RNG.Next();
        }

        public int Next(int maxValue)
        {
            return RNG.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return RNG.Next(minValue, maxValue);
        }

        public void NextBytes(byte[] buffer)
        {
            RNG.NextBytes(buffer);
        }

        public double NextDouble()
        {
            return RNG.NextDouble();
        }
    }

    public static class GRNG
    {
        public static IRndGen RNG = new RndGen();
    }

    public static class TSPExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = GRNG.RNG.Next(n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void SwapValues<T>(this IList<T> list, int left, int right)
        {
            var tmp = list[left];
            list[left] = list[right];
            list[right] = tmp;
        }
    }

    public class MutatorFactory : IMutatorFactory<IMutator<PathGenome>>
    {
        public IMutator<PathGenome> createMutator(MutationType mut_type)
        {
            switch(mut_type)
            {
                case MutationType.Exchange:
                    return new ExchangeMutator();

                case MutationType.Displacement:
                    return new DisplacementMutator();

                case MutationType.Insertion:
                    return new InsertionMutator();

                case MutationType.Inversion:
                    return new InversionMutator();

                case MutationType.Scramble:
                    return new ScrambleMutator();

                case MutationType.DisplacedInversion:
                    return new DisplacedInversionMutator();

                default:
                    throw new ArgumentException($"Don't know how to make mutator {mut_type}");
            }
        }
    }

    public class CrossoverFactory : ICrossoverFactory<ICrossOver<PathGenome>>
    {
        public ICrossOver<PathGenome> createCrossover(CrossoverType co_type)
        {
            switch(co_type)
            {
                case CrossoverType.OrderBased:
                    return new OrderBasedCrossover();

//                case CrossoverType.PartiallyMatched:
//                    return new PartiallyMatchedCrossover();

                case CrossoverType.PositionBased:
//                    return new PositionBasedCrossover();

                default:
                    throw new ArgumentException($"Don't know how to make crossover {co_type}");
            }
        }
    }

    class OrderBasedCrossover : ICrossOver<PathGenome>
    {
        public Tuple<PathGenome, PathGenome> Crossover(PathGenome mom, PathGenome dad)
        {
            var baby1 = new PathGenome(mom, mom.MutationType, mom.CrossoverType);
            var baby2 = new PathGenome(dad, dad.MutationType, dad.CrossoverType);

            var temp_cities = new List<int>();
            var positions = new List<int>();

            int pos = GRNG.RNG.Next(mom.Data.Count - 2);

            while(pos < mom.Data.Count)
            {
                positions.Add(pos);
                temp_cities.Add(mom.Data[pos]);
                pos += GRNG.RNG.Next(1, mom.Data.Count - pos);
            }

            int city_pos = 0;
            for(int cit = 0; cit < baby2.Data.Count; ++cit)
            {
                for(int i = 0; i < temp_cities.Count; ++i)
                {
                    if(baby2.Data[cit] == temp_cities[i])
                    {
                        baby2.Data[cit] = temp_cities[city_pos];
                        ++city_pos;
                        break;
                    }
                }
            }
            temp_cities.Clear();
            city_pos = 0;

            for (int i = 0; i < positions.Count; ++i)
            {
                temp_cities.Add(dad.Data[positions[i]]);
            }

            for(int cit = 0; cit < baby1.Data.Count; ++cit)
            {
                for(int i = 0; i < temp_cities.Count; ++i)
                {
                    if(baby1.Data[cit] == temp_cities[i])
                    {
                        baby1.Data[cit] = temp_cities[city_pos];
                        ++city_pos;
                        break;
                    }
                }
            }
            return Tuple.Create(baby1, baby2);
        }
    }

    public class PositionBasedCrossover : ICrossOver<PathGenome>
    {
        public Tuple<PathGenome, PathGenome> Crossover(PathGenome mom, PathGenome dad)
        {
            var empty_list = Enumerable.Repeat(-1, mom.Data.Count);
            var baby1 = new PathGenome(empty_list.ToList(), mom.MutationType, mom.CrossoverType);
            var baby2 = new PathGenome(empty_list.ToList(), dad.MutationType, dad.CrossoverType);

            var positions = new List<int>();
            int pos = GRNG.RNG.Next(mom.Data.Count - 2);
            while(pos < mom.Data.Count)
            {
                positions.Add(pos);
                pos += GRNG.RNG.Next(1, mom.Data.Count - pos);
            }
            positions.ForEach((idx) =>
            {
                baby1.Data[idx] = mom.Data[idx];
                baby2.Data[idx] = dad.Data[idx];
            });

            var dad_values = new Stack<int>(dad.Data.Except(baby1.Data).Reverse());
            baby1.Data = baby1.Data.Select(value =>
            {
                return value == -1 ? dad_values.Pop() : value;
            }).ToList();

            var mom_values = new Stack<int>(mom.Data.Except(baby2.Data).Reverse());

            baby2.Data = baby2.Data.Select(value =>
            {
                return value == -1 ? mom_values.Pop() : value;
            }).ToList();
            return Tuple.Create(baby1, baby2);
        }
    }

    public class DisplacementMutator : IMutator<PathGenome>
    {
        public void mutate(PathGenome genome)
        {
            const int min_span = 3;
            var span = GRNG.RNG.ChooseSpan(min_span, genome.Data.Count);
            int span_size = span.Item2 - span.Item1;

            if (span_size == genome.Data.Count) return;

            int insert_point = GRNG.RNG.Next(genome.Data.Count - span_size);
            int insert_end = insert_point + span_size;

            var span_values = new Stack<int>(genome.Data.Skip(span.Item1).Take(span_size).Reverse());
            int span_start = span.Item1;
            int span_end = span.Item2;

            var non_span_before_start = genome.Data.Take(span_start);
            var non_span_after_end = genome.Data.Skip(span_end).ToList();
            var non_span_values = new Stack<int>(non_span_before_start.Concat(non_span_after_end).Reverse());

            genome.Data = Enumerable.Range(0, genome.Data.Count).Select(idx =>
            {
                if(idx >= insert_point && idx < insert_end)
                {
                    return span_values.Pop();
                }
                else
                {
                    return non_span_values.Pop();
                }
            }).ToList();
        }
    }

    public class DisplacedInversionMutator : IMutator<PathGenome>
    {
        public void mutate(PathGenome genome)
        {
            const int min_span = 3;
            var span = GRNG.RNG.ChooseSpan(min_span, genome.Data.Count);
            int span_size = span.Item2 - span.Item1;

            if (span_size == genome.Data.Count) return;

            int insert_point = GRNG.RNG.Next(genome.Data.Count - span_size);
            int insert_end = insert_point + span_size;

            int span_start = span.Item1;
            int span_end = span.Item2;
            var span_values = new Stack<int>(genome.Data.Skip(span.Item1).Take(span_size));

            var non_span_before_start = genome.Data.Take(span_start);
            var non_span_after_end = genome.Data.Skip(span_end).ToList();
            var non_span_values = new Stack<int>(non_span_before_start.Concat(non_span_after_end).Reverse());

            genome.Data = Enumerable.Range(0, genome.Data.Count).Select(idx =>
            {
                if(idx >= insert_point && idx < insert_end)
                {
                    return span_values.Pop();
                }
                else
                {
                    return non_span_values.Pop();
                }
            }).ToList();
        }
    }

    public class InversionMutator : IMutator<PathGenome>
    {
        public void mutate(PathGenome genome)
        {
            const int min_span = 3;
            var span = GRNG.RNG.ChooseSpan(min_span, genome.Data.Count);
            var start = span.Item1;
            var end = span.Item2;
            while(start < end)
            {
                genome.Data.SwapValues(start, end);
                ++start;
                --end;
            }
        }
    }

    public class ScrambleMutator : IMutator<PathGenome>
    {
        public void mutate(PathGenome genome)
        {
            const int min_span = 3;
            var span = GRNG.RNG.ChooseSpan(min_span, genome.Data.Count);
            int num_swaps = span.Item2 - span.Item1;
            while(num_swaps > 0)
            {
                int pos1 = GRNG.RNG.Next(span.Item1, span.Item2);
                int pos2 = GRNG.RNG.Next(span.Item1, span.Item2);
                genome.Data.SwapValues(pos1, pos2);
                --num_swaps;
            }
        }
    }

    public class ExchangeMutator : IMutator<PathGenome>
    {
        public void mutate(PathGenome genome)
        {
            int pos1 = GRNG.RNG.Next(0, genome.Data.Count);
            int pos2 = pos1;
            while(pos2 == pos1)
            {
                pos2 = GRNG.RNG.Next(0, genome.Data.Count);
            }
            int value1 = genome.Data[pos1];
            int tmp = genome.Data[pos2];
            genome.Data[pos1] = tmp;
            genome.Data[pos2] = value1;
        }
    }

    public class InsertionMutator : IMutator<PathGenome>
    {
        public void mutate(PathGenome genome)
        {
            int rm_idx = GRNG.RNG.Next(genome.Data.Count);
            var item = genome.Data[rm_idx];
            genome.Data.RemoveAt(rm_idx);
            var ins_idx = GRNG.RNG.Next(genome.Data.Count);
            genome.Data.Insert(ins_idx, item);
        }
    }

    public class PathGenome : IGenome<int, PathGenome>
    {
        private static IMutatorFactory<IMutator<PathGenome>> MutFactory = new MutatorFactory();
        private static ICrossoverFactory<ICrossOver<PathGenome>> CrossoverFactory = new CrossoverFactory();
        private IList<int> m_data;
        private IMutator<PathGenome> m_mutator;
        private ICrossOver<PathGenome> m_crossover;
        private MutationType m_mutation_type;
        private CrossoverType m_crossover_type;

        public override string ToString()
        {
            return $"Fitness: {Fitness}, path: {Data}";
        }

        public PathGenome(IList<int> data, MutationType mut_type = MutationType.Exchange, CrossoverType co_type = CrossoverType.OrderBased)
        {
            Data = data;
            m_mutator = MutFactory.createMutator(mut_type);
            m_mutation_type = mut_type;
            m_crossover_type = co_type;
            m_crossover = CrossoverFactory.createCrossover(co_type);
        }

        public PathGenome(PathGenome pg, MutationType mut_type = MutationType.Exchange, CrossoverType co_type = CrossoverType.OrderBased)
        {
            m_data = new List<int>(pg.Data);
            m_mutator = MutFactory.createMutator(mut_type);
            m_mutation_type = mut_type;
            m_crossover_type = co_type;
            m_crossover = CrossoverFactory.createCrossover(co_type);
        }

        public PathGenome(int num_cities, MutationType mut_type = MutationType.Exchange, CrossoverType co_type = CrossoverType.OrderBased)
        {
            if(num_cities <= 1)
            {
                throw new ArgumentException("Number of cities must be a positive value not less than two");
            }
            IList<int> indices = Enumerable.Range(0, num_cities).ToList();
            indices.Shuffle();
            Data = new List<int>(indices);
            m_mutator = MutFactory.createMutator(mut_type);
            m_crossover = CrossoverFactory.createCrossover(co_type);
            m_mutation_type = mut_type;
            m_crossover_type = co_type;
        }

        public MutationType MutationType
        {
            get
            {
                return m_mutation_type;
            }
        }

        public CrossoverType CrossoverType
        {
            get
            {
                return m_crossover_type;
            }
        }

        public IList<int> Data
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;
            }
        }

        public double Fitness
        {
            get; set;
        }

        public Tuple<PathGenome, PathGenome> CrossOver(PathGenome other, double crossOverRate)
        {
            if (this == other || GRNG.RNG.NextDouble() > crossOverRate)
            {
                return null;
            }

            return m_crossover.Crossover(this, other);

            PathGenome baby1 = new PathGenome(this, MutationType);
            PathGenome baby2 = new PathGenome(other, MutationType);

            int start = GRNG.RNG.Next(0, Data.Count-2);
            int end = GRNG.RNG.Next(start, Data.Count);

            for(int pos = start; pos < end+1; ++pos)
            {
                int gene1 = baby1.Data[pos];
                int gene2 = baby2.Data[pos];
                if(gene1 != gene2)
                {
                    int pos_gene2 = baby1.Data.IndexOf(gene2);
                    baby1.Data.SwapValues(pos, pos_gene2);

                    int pos_gene1 = baby2.Data.IndexOf(gene1);
                    baby2.Data.SwapValues(pos_gene1, pos);
                }
            }
            Tuple<PathGenome, PathGenome> result = Tuple.Create(baby1, baby2);
            return result;
        }

        public void Mutate(double mutationRate)
        {
            if(GRNG.RNG.NextDouble() <= mutationRate)
            {
                m_mutator.mutate(this);
            }
        }
    }

    public class Population : IPopulation<PathGenome>
    {
        public int Generation
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<PathGenome> getGeneration(int generation)
        {
            throw new NotImplementedException();
        }

        public double getGenerationAverage(int genIdx)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double> getGenerationsAverages()
        {
            throw new NotImplementedException();
        }
    }

    class RouletteWheelSelection : ISelectionStrategy<PathGenome>
    {
        public PathGenome Select(IList<PathGenome> population)
        {
            PathGenome selected_genome = null;
            double total_fitness = population.Sum((genome) => genome.Fitness);
            double slice = GRNG.RNG.NextDouble() * total_fitness;
            double accumulating_fitness = 0.0;

            foreach(PathGenome genome in population)
            {
                accumulating_fitness += genome.Fitness;
                if(accumulating_fitness >= slice)
                {
                    selected_genome = genome;
                    break;
                }
            }
            Debug.Assert(selected_genome != null);
            return selected_genome;
        }
    }

    class TSPGenAlg : IGenAlg<PathGenome>
    {
        public static double CROSSOVER_RATE = 0.75;
        public static double MUTATION_RATE = 0.25;
        public static int NUM_BEST_TO_ADD = 2;

        private IList<PathGenome> m_population;
        private double m_shortest_route;
        private double m_longest_route;
        private TSPMap m_map;
        private PathGenome m_fittest_genome;
        private ISelectionStrategy<PathGenome> m_selection_strategy;

        public bool Done { get; set; }
        public int GenerationNumber { get; private set; }
        public PathGenome FittestGenome { get { return m_fittest_genome; } }

        public TSPGenAlg(int num_cities, int population_size, TSPMap map)
        {
            m_population = Enumerable.Range(0, population_size).Select((_) =>
            {
                return new PathGenome(num_cities, MutationType.Insertion);
            }).ToList();
            m_shortest_route = double.MaxValue;
            m_longest_route = double.MinValue;
            m_map = map;
            m_selection_strategy = new RouletteWheelSelection();
            Done = false;
            GenerationNumber = 1;
        }

        public IList<PathGenome> CreatePopulation(IList<PathGenome> generation, ISelectionStrategy<PathGenome> strategy)
        {
            IList<PathGenome> new_generation = new List<PathGenome> { };

            while(new_generation.Count < generation.Count-2)
            {
                var dad = strategy.Select(generation);
                var mom = strategy.Select(generation);
                var kids = dad.CrossOver(mom, CROSSOVER_RATE);
                if (kids == null) continue;

                kids.Item1.Mutate(TSPGenAlg.MUTATION_RATE);
                new_generation.Add(kids.Item1);
                if (new_generation.Count < generation.Count-1)
                {
                    kids.Item2.Mutate(TSPGenAlg.MUTATION_RATE);
                    new_generation.Add(kids.Item2);
                }
            }
            new_generation.Insert(GRNG.RNG.Next(generation.Count-2), FittestGenome);
            new_generation.Add(FittestGenome);
            return new_generation;
        }

        private void Reset()
        {
            m_shortest_route = double.MaxValue;
            m_longest_route = double.MinValue;
        }

        public void Epoch()
        {
            Reset();
            CalculateFitness();
            if(m_shortest_route <= m_map.BestPossibleRoute)
            {
                Done = true;
                return;
            }

            m_population = CreatePopulation(m_population, m_selection_strategy);
            GenerationNumber++;
        }

        public void CalculateFitness()
        {
            foreach(PathGenome genome in m_population)
            {
                double tour_length = m_map.CalculateTourLength(genome);
                genome.Fitness = tour_length;

                if(tour_length < m_shortest_route)
                {
                    m_shortest_route = tour_length;
                    m_fittest_genome = genome;
                }
                m_longest_route = Math.Max(m_longest_route, tour_length);
            }

            foreach(PathGenome genome in m_population)
            {
                genome.Fitness = m_longest_route - genome.Fitness;
            }
        }
    }
}