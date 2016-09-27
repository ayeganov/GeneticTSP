using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticTSP;
using System.Diagnostics;

namespace TSPGenTest
{
    [TestClass]
    public class TSPGenAlgTest
    {
        [TestMethod]
        public void TestPathGenomeCrossOverConstructor()
        {
            IGenome<int> genome1 = new PathGenome(100);
            Assert.AreEqual(100, genome1.Data.Count);
        }

        [TestMethod]
        public void TestGenomeCrossOver()
        {
            IGenome<int> genome1 = new PathGenome(10);
            IGenome<int> genome2 = new PathGenome(10);
            var babies = genome1.CrossOver(genome1, 1.0);
            Assert.IsNull(babies);
            babies = genome1.CrossOver(genome2, 1.0);
            Assert.AreNotEqual(genome1.Data, babies.Item1.Data);
            Assert.AreNotEqual(genome2.Data, babies.Item1.Data);

            Assert.AreNotEqual(genome1.Data, babies.Item2.Data);
            Assert.AreNotEqual(genome2.Data, babies.Item2.Data);
            Trace.WriteLine(babies);
        }
    }
}
