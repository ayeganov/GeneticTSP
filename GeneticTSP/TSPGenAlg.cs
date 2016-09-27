using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GeneticTSP
{
    delegate void Swap<T>(IList<T> list, int left, int right);

    public class GRNG
    {
        public static Random RNG = new Random(DateTime.Now.Millisecond);
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
    }

    public class PathGenome : IGenome<int> 
    {
        private IList<int> m_data;

        public override string ToString()
        {
            return $"Fitness: {Fitness}, path: {Data}";
        }

        public PathGenome(IList<int> data)
        {
            Data = data;
            Data.Shuffle();
        }

        public PathGenome(IGenome<int> pg)
        {
            m_data = new List<int>(pg.Data);
        }

        public PathGenome(int num_cities)
        {
            if(num_cities <= 1)
            {
                throw new ArgumentException("Number of cities must be a positive value not less than two");
            }
            var indices = Enumerable.Range(0, num_cities).ToList();
            indices.Shuffle();
            Data = new List<int>(indices);
        }

        public IList<int> Data
        {
            get
            {
                return m_data;
            }
            private set
            {
                m_data = value;
            }
        }

        public double Fitness
        {
            get; set;
        }

        public Tuple<IGenome<int>, IGenome<int>> CrossOver(IGenome<int> other, double crossOverRate)
        {
            if (this == other || GRNG.RNG.NextDouble() > crossOverRate)
            {
                return null;
            }

            IGenome<int> baby1 = new PathGenome(this);
            IGenome<int> baby2 = new PathGenome(other);

            Swap<int> swap = (IList<int> list, int left, int right) =>
            {
                var tmp = list[left];
                list[left] = list[right];
                list[right] = tmp;
            };

            int start = GRNG.RNG.Next(0, Data.Count-2);
            int end = GRNG.RNG.Next(start, Data.Count);

            for(int pos = start; pos < end+1; ++pos)
            {
                int gene1 = baby1.Data[pos];
                int gene2 = baby2.Data[pos];
                if(gene1 != gene2)
                {
                    int pos_gene2 = baby1.Data.IndexOf(gene2);
                    swap(baby1.Data, pos, pos_gene2);

                    int pos_gene1 = baby2.Data.IndexOf(gene1);
                    swap(baby2.Data, pos_gene1, pos);
                }
            }
            Tuple<IGenome<int>, IGenome<int>> result = Tuple.Create(baby1, baby2);
            return result;
        }

        public void Mutate(double mutationRate)
        {
            if(GRNG.RNG.NextDouble() <= mutationRate)
            {
                int pos1 = GRNG.RNG.Next(0, Data.Count);
                int pos2 = pos1;
                while(pos2 == pos1)
                {
                    pos2 = GRNG.RNG.Next(0, Data.Count);
                }
                int value1 = Data[pos1];
                int tmp = Data[pos2];
                Data[pos1] = tmp;
                Data[pos2] = value1;
            }
        }
    }

    class RouletteWheelSelection<T> : ISelectionStrategy<T>
    {
        public IGenome<T> Select(IList<IGenome<T>> population)
        {
            IGenome<T> selected_genome = null;
            double total_fitness = population.Sum((genome) => genome.Fitness);
            double slice = GRNG.RNG.NextDouble() * total_fitness;
            double accumulating_fitness = 0.0;

            foreach(IGenome<T> genome in population)
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

    class TSPGenAlg : IGenAlg<int>
    {
        public static double CROSSOVER_RATE = 0.75;
        public static double MUTATION_RATE = 0.3;
        public static int NUM_BEST_TO_ADD = 2;

        private IList<IGenome<int>> m_population;
        private double m_shortest_route;
        private double m_longest_route;
        private TSPMap m_map;
        private IGenome<int> m_fittest_genome;
        private ISelectionStrategy<int> m_selection_strategy;

        public bool Done { get; set; }
        public int GenerationNumber { get; private set; }
        public IGenome<int> FittestGenome { get { return m_fittest_genome; } }

        public TSPGenAlg(int num_cities, int population_size, TSPMap map)
        {
            var res = Enumerable.Range(0, population_size).Select((_) => { return new PathGenome(num_cities); });
            m_population = new List<IGenome<int>>(res);
            m_shortest_route = double.MaxValue;
            m_longest_route = double.MinValue;
            m_map = map;
            m_selection_strategy = new RouletteWheelSelection<int>();
            Done = false;
            GenerationNumber = 1;
        }

        public IList<IGenome<int>> CreatePopulation(IList<IGenome<int>> generation, ISelectionStrategy<int> strategy)
        {
            IList<IGenome<int>> new_generation = new List<IGenome<int>> { };

            while(new_generation.Count < generation.Count-1)
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
            new_generation.Insert(GRNG.RNG.Next(generation.Count), FittestGenome);
//            new_generation.Add(FittestGenome);
//            new_generation.Add(FittestGenome);
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
//                Console.WriteLine("Solution found");
                Done = true;
                return;
            }

            m_population = CreatePopulation(m_population, m_selection_strategy);
            GenerationNumber++;
        }

        public void CalculateFitness()
        {
            foreach(IGenome<int> genome in m_population)
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

            foreach(IGenome<int> genome in m_population)
            {
                genome.Fitness = m_longest_route - genome.Fitness;
            }
//            Console.WriteLine($"Shorted route is {m_shortest_route} Longest is {m_longest_route}");
            double average_length = m_population.Average(genome =>
            {
                return genome.Fitness;
            });
//            Console.WriteLine($"Average route length is {average_length}");
        }
    }
}