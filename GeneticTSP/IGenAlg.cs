using System;
using System.Collections.Generic;

namespace GeneticTSP
{
    interface IGenAlg<T>
    {
        void Epoch();
        void CalculateFitness();
        IList<IGenome<T>> CreatePopulation(IList<IGenome<T>> generation, ISelectionStrategy<T> strategy);
    }

    public interface IGenome<T>
    {
        IList<T> Data { get; }
        double Fitness { get; set; }

        Tuple<IGenome<T>, IGenome<T>> CrossOver(IGenome<T> other, double crossOverRate);
        void Mutate(double mutationRate);
    }

    interface ISelectionStrategy<T>
    {
        IGenome<T> Select(IList<IGenome<T>> population);
    }

    struct FitnessStat
    {
        double FitnessAverage { get; set; }
        double FitnessStdDev { get; set; }
    }

    interface IGenStat<T>
    {
        int Generation { get; set; }
        IEnumerable<T> getGeneration(int generation);
        double getGenerationAverage(int genIdx);
        IEnumerable<double> getGenerationsAverages();

    }
}