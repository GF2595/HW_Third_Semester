using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphNamespace;

namespace HW18_09Namespace.Tests
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void ConsequenceExistanceTest()
        {
            Graph graph = new Graph("..\\..\\testConfig1.txt");
            Assert.AreEqual(false, graph.CheckDestroyingConsequenceExistence());

            graph = new Graph("..\\..\\testConfig2.txt");
            Assert.AreEqual(false, graph.CheckDestroyingConsequenceExistence());

            graph = new Graph("..\\..\\testConfig3.txt");
            Assert.AreEqual(true, graph.CheckDestroyingConsequenceExistence());
        }
    }
}
