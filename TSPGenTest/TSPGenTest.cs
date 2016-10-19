using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticTSP;
using System.Diagnostics;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace TSPGenTest
{
    [TestClass]
    public class TSPGenAlgTest
    {
        [TestCleanup()]
        public void CleanUp()
        {
            GRNG.RNG = new RndGen();
        }

        [TestMethod]
        public void TestPathGenomeCrossOverConstructor()
        {
            IGenome<int, PathGenome> genome1 = new PathGenome(100);
            Assert.AreEqual(100, genome1.Data.Count);
        }

        [TestMethod]
        public void TestGenomeCrossOver()
        {
            PathGenome genome1 = new PathGenome(10);
            PathGenome genome2 = new PathGenome(10);
            var babies = genome1.CrossOver(genome1, 1.0);
            Assert.IsNull(babies);
            babies = genome1.CrossOver(genome2, 1.0);
            Assert.AreNotEqual(genome1.Data, babies.Item1.Data);
            Assert.AreNotEqual(genome2.Data, babies.Item1.Data);

            Assert.AreNotEqual(genome1.Data, babies.Item2.Data);
            Assert.AreNotEqual(genome2.Data, babies.Item2.Data);
            Trace.WriteLine(babies);
        }

        [TestMethod]
        public void TestPositionBasedCrossover()
        {
            PathGenome dad = new PathGenome(new List<int> { 2, 5, 0, 3, 6, 1, 4, 7 });
            PathGenome mom = new PathGenome(new List<int> { 3, 4, 0, 7, 2, 5, 1, 6 });
            ICrossOver<PathGenome> pbx = new PositionBasedCrossover();

            var mock = new Mock<IRndGen>();
            // make sure span will be from 0 to 4
            mock.Setup(rnd => rnd.Next(mom.Data.Count - 2)).Returns(1);
            mock.Setup(rnd => rnd.Next(1, mom.Data.Count - 1)).Returns(1);
            mock.Setup(rnd => rnd.Next(1, mom.Data.Count - 2)).Returns(3);
            mock.Setup(rnd => rnd.Next(1, mom.Data.Count - 5)).Returns(90);
            GRNG.RNG = mock.Object;

            var kids = pbx.Crossover(mom, dad);

            var expected_kid1 = new List<int> { 2, 4, 0, 3, 6, 5, 1, 7 };
            var expected_kid2 = new List<int> { 3, 5, 0, 4, 7, 1, 2, 6 };

            Assert.IsTrue(AreListsEqual(expected_kid1, kids.Item1.Data));
            Assert.IsTrue(AreListsEqual(expected_kid2, kids.Item2.Data));
        }

        private bool AreListsEqual<T>(IList<T> first, IList<T> second) where T : IComparable
        {
            if (first.Count != second.Count) return false;
            return first.Zip(second, (f, s) =>
            {
                return Tuple.Create(f, s);
            }).All(t =>
            {
                return t.Item1.CompareTo(t.Item2) == 0;
            });
        }

        [TestMethod]
        public void TestDisplacementMutatorSpanBeginning()
        {
            int num_cities = 10;

            var mock = new Mock<IRndGen>();
            // make sure span will be from 0 to 4
            mock.Setup(rnd => rnd.ChooseSpan(3, num_cities)).Returns(() => Tuple.Create(0, 4));
            // when picking insertion point make sure it returns 5
            mock.Setup(rnd => rnd.Next(6)).Returns(5);
            GRNG.RNG = mock.Object;

            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());
            IMutator<PathGenome> disp_mut = new DisplacementMutator();
            var expected_sequnce = new List<int> { 4, 5, 6, 7, 8, 0, 1, 2, 3, 9 };
            disp_mut.Mutate(genome);

            Assert.IsTrue(AreListsEqual(expected_sequnce, genome.Data));
        }

        [TestMethod]
        public void TestDisplacementMutatorSpanMiddle()
        {
            int num_cities = 10;

            var mock = new Mock<IRndGen>();
            // make sure span will be from 4 to 8
            mock.Setup(rnd => rnd.ChooseSpan(3, num_cities)).Returns(() => Tuple.Create(4, 8));
            // when picking insertion point make sure it returns 0
            mock.Setup(rnd => rnd.Next(6)).Returns(0);
            GRNG.RNG = mock.Object;

            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());
            IMutator<PathGenome> disp_mut = new DisplacementMutator();
            var expected_sequnce = new List<int> { 4, 5, 6, 7, 0, 1, 2, 3, 8, 9 };
            disp_mut.Mutate(genome);

            Assert.IsTrue(AreListsEqual(expected_sequnce, genome.Data));
        }

        [TestMethod]
        public void TestDisplacementMutatorSpanEnd()
        {
            int num_cities = 10;

            var mock = new Mock<IRndGen>();
            // make sure span will be from 8 to 10
            mock.Setup(rnd => rnd.ChooseSpan(3, num_cities)).Returns(() => Tuple.Create(8, 10));
            // when picking insertion point make sure it returns 3
            mock.Setup(rnd => rnd.Next(8)).Returns(3);
            GRNG.RNG = mock.Object;

            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());
            IMutator<PathGenome> disp_mut = new DisplacementMutator();
            var expected_sequnce = new List<int> { 0, 1, 2, 8, 9, 3, 4, 5, 6, 7 };
            disp_mut.Mutate(genome);

            Assert.IsTrue(AreListsEqual(expected_sequnce, genome.Data));
        }

        [TestMethod]
        public void TestDisplacementMutatorSpanFullList()
        {
            int num_cities = 10;

            var mock = new Mock<IRndGen>();
            // make sure span will be from 0 to 10
            mock.Setup(rnd => rnd.ChooseSpan(3, num_cities)).Returns(() => Tuple.Create(0, 10));
            GRNG.RNG = mock.Object;

            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());
            IMutator<PathGenome> disp_mut = new DisplacementMutator();
            var expected_sequnce = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            disp_mut.Mutate(genome);

            Assert.IsTrue(AreListsEqual(expected_sequnce, genome.Data));
        }

        [TestMethod]
        public void TestScrambleMutator()
        {
            int num_cities = 10;
            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());

            IMutator<PathGenome> scramble_mut = new ScrambleMutator();
            scramble_mut.Mutate(genome);

            var equal = true;
            while(equal)
            {
                scramble_mut.Mutate(genome);
                equal = AreListsEqual(cities.ToList(), genome.Data);
            }
            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void TestInsertionMutator()
        {
            int num_cities = 10;
            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());

            var mock = new Mock<IRndGen>();
            mock.Setup(rnd => rnd.Next(num_cities)).Returns(3);
            mock.Setup(rnd => rnd.Next(num_cities-1)).Returns(6);
            GRNG.RNG = mock.Object;

            IMutator<PathGenome> insertion_mut = new InsertionMutator();
            insertion_mut.Mutate(genome);
            var equal = AreListsEqual(cities.ToList(), genome.Data);
            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void TestInversionMutator()
        {
            int num_cities = 10;
            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());

            var mock = new Mock<IRndGen>();
            mock.Setup(rnd => rnd.ChooseSpan(3, num_cities)).Returns(() => Tuple.Create(3, 8));
            GRNG.RNG = mock.Object;

            IMutator<PathGenome> inversion_mut = new InversionMutator();
            inversion_mut.Mutate(genome);

            var expected_sequnce = new List<int> { 0, 1, 2, 8, 7, 6, 5, 4, 3, 9 };
            var equal = AreListsEqual(expected_sequnce, genome.Data);
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void TestDisplacedInversionMutator()
        {
            int num_cities = 10;

            var mock = new Mock<IRndGen>();
            // make sure span will be from 4 to 8
            mock.Setup(rnd => rnd.ChooseSpan(3, num_cities)).Returns(() => Tuple.Create(4, 8));
            // when picking insertion point make sure it returns 0
            mock.Setup(rnd => rnd.Next(6)).Returns(0);
            GRNG.RNG = mock.Object;

            var cities = Enumerable.Range(0, num_cities);
            PathGenome genome = new PathGenome(cities.ToList());
            IMutator<PathGenome> disp_mut = new DisplacedInversionMutator();
            var expected_sequnce = new List<int> { 7, 6, 5, 4, 0, 1, 2, 3, 8, 9 };
            disp_mut.Mutate(genome);

            Assert.IsTrue(AreListsEqual(expected_sequnce, genome.Data));
        }
    }
}