using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkNamespace;
using System.Collections.Generic;

namespace HW11_09Namespace.Tests
{
    [TestClass]
    public class NetworkTest
    {
        [TestMethod]
        public void TickAmountTest()
        {
            Network network = new Network("..\\..\\testConfig1.txt");

            Assert.AreEqual(0, network.TickAmountStatistics());

            network.Run();

            Assert.AreEqual(4, network.TickAmountStatistics());

            network = new Network("..\\..\\testConfig2.txt");

            Assert.AreEqual(0, network.TickAmountStatistics());

            network.Run();

            Assert.AreEqual(1, network.TickAmountStatistics());
        }

        [TestMethod]
        public void VirusStatisticsTest()
        {
            Network network = new Network("..\\..\\testConfig1.txt");

            List<string> statistics = network.VirusesStatistics();
            foreach (string str in statistics)
            {
                Assert.AreEqual("virus 1", str);
            }

            network.Run();

            statistics = network.VirusesStatistics();
            foreach (string str in statistics)
            {
                Assert.AreEqual("virus 1 2 3 4", str);
            }

            network = new Network("..\\..\\testConfig2.txt");

            statistics = network.VirusesStatistics();

            Assert.AreEqual("virus 1", statistics[0]);
            Assert.AreEqual("virus2 2", statistics[1]);

            network.Run();

            statistics = network.VirusesStatistics();

            Assert.AreEqual("virus 1", statistics[0]);
            Assert.AreEqual("virus2 2", statistics[1]);
        }

        [TestMethod]
        public void StepVirusStatisticsTest()
        {
            Network network = new Network("..\\..\\testConfig1.txt");

            List<string> statistics = network.VirusesStatistics();

            foreach (string str in statistics)
            {
                Assert.AreEqual("virus 1", str);
            }

            network.Step();

            statistics = network.VirusesStatistics();
            foreach (string str in statistics)
            {
                Assert.AreEqual("virus 1 2", str);
            }

            network.Step();

            statistics = network.VirusesStatistics();
            foreach (string str in statistics)
            {
                Assert.AreEqual("virus 1 2 3", str);
            }

            network.Step();

            statistics = network.VirusesStatistics();
            foreach (string str in statistics)
            {
                Assert.AreEqual("virus 1 2 3 4", str);
            }
        }
    }
}
