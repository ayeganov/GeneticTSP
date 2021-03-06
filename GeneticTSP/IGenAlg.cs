﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace GeneticTSP
{
    public interface IRndGen
    {
        //
        // Summary:
        //     Returns a non-negative random integer.
        //
        // Returns:
        //     A 32-bit signed integer that is greater than or equal to 0 and less than System.Int32.MaxValue.
        int Next();
        //
        // Summary:
        //     Returns a non-negative random integer that is less than the specified maximum.
        //
        // Parameters:
        //   maxValue:
        //     The exclusive upper bound of the random number to be generated. maxValue must
        //     be greater than or equal to 0.
        //
        // Returns:
        //     A 32-bit signed integer that is greater than or equal to 0, and less than maxValue;
        //     that is, the range of return values ordinarily includes 0 but not maxValue. However,
        //     if maxValue equals 0, maxValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     maxValue is less than 0.
        int Next(int maxValue);
        //
        // Summary:
        //     Returns a random integer that is within a specified range.
        //
        // Parameters:
        //   minValue:
        //     The inclusive lower bound of the random number returned.
        //
        //   maxValue:
        //     The exclusive upper bound of the random number returned. maxValue must be greater
        //     than or equal to minValue.
        //
        // Returns:
        //     A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        //     that is, the range of return values includes minValue but not maxValue. If minValue
        //     equals maxValue, minValue is returned.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     minValue is greater than maxValue.
        int Next(int minValue, int maxValue);
        //
        // Summary:
        //     Fills the elements of a specified array of bytes with random numbers.
        //
        // Parameters:
        //   buffer:
        //     An array of bytes to contain random numbers.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        void NextBytes(byte[] buffer);
        //
        // Summary:
        //     Returns a random floating-point number that is greater than or equal to 0.0,
        //     and less than 1.0.
        //
        // Returns:
        //     A double-precision floating point number that is greater than or equal to 0.0,
        //     and less than 1.0.
        double NextDouble();

        Tuple<int, int> ChooseSpan(int min_span, int max_span);
    }

    interface IGenAlg<TGenomeType>
    {
        string Epoch();
        void CalculateFitness();
        IGeneration<TGenomeType> CreateGeneration(IGeneration<TGenomeType> generation, ISelectionStrategy<TGenomeType> strategy);
        void SetSelectionStrategy(ISelectionStrategy<TGenomeType> strategy);
        void SetMutationType(MutationType type);
        void SetCrossoverType(CrossoverType type);
        void SetScalingType(IFitnessScaler<TGenomeType> scaler);
        bool Elitism { get; }

        IFitnessScaler<TGenomeType> FitnessScaler { get; }
    }

    public interface IGenome<TData, TSubtype> where TSubtype : IGenome<TData, TSubtype>
    {
        IList<TData> Data { get; }
        /**
         * <summary> Fitness of this genome </summary>
         */
        double Fitness { get; set; }

        Tuple<TSubtype, TSubtype> CrossOver(TSubtype other, double crossOverRate);
        void Mutate(double mutationRate);
    }

    interface ISelectionStrategy<TGenomeType>
    {
        TGenomeType Select(IGeneration<TGenomeType> generation);
        IEnumerable<TGenomeType> SelectN(IGeneration<TGenomeType> generation, int n);
    }

    public interface IGeneration<TGenomeType> : IEnumerable<TGenomeType>
    {
        double AverageFitness
        {
            get;
        }

        double StandardDeviation
        {
            get;
        }

        TGenomeType BestGenome
        {
            get;
        }

        TGenomeType WorstGenome
        {
            get;
        }

        double TotalFitness
        {
            get;
        }

        double BestFitness
        {
            get;
        }

        double WorstFitness
        {
            get;
        }

        TGenomeType this[int index]
        {
            get;
        }

        int Size
        {
            get;
        }

        void CalculateStats();

        void AddGenome(TGenomeType genome);
    }

    public enum MutationType
    {
        Exchange,
        Displacement,
        Scramble,
        Insertion,
        Inversion,
        DisplacedInversion,
    }

    public enum CrossoverType
    {
        PartiallyMatched,
        OrderBased,
        PositionBased,
    }

    public enum ScalerType
    {
        Sigma,
        Rank,
        Boltzmann
    }

    public interface IMutator<TGenomeType>
    {
        void Mutate(TGenomeType genome);
    }

    public interface ICrossOver<TGenomeType>
    {
        Tuple<TGenomeType, TGenomeType> Crossover(TGenomeType mom, TGenomeType dad);
    }

    public interface IFitnessScaler<TGenomeType>
    {
        event Action<double> OnScale;
        void ScalePopulationFitness(IGeneration<TGenomeType> generation);
    }

    public interface IMutatorFactory<TMutator>
    {
        TMutator CreateMutator(MutationType mut_type);
    }

    public interface ICrossoverFactory<TCrossover>
    {
        TCrossover CreateCrossover(CrossoverType co_type);
    }

    public interface IFitnessScalerFactory<TGenomeType>
    {
        IFitnessScaler<TGenomeType> CreateFitnessScaler(ScalerType scale_type);
    }

}