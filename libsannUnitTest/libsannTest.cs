using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace libsannUnitTest
{
    using libsannNET;

    [TestClass]
    public class libsannTest
    {
        Mlp mlpNetwork;

        public libsannTest()
        {
            uint[] hlayers = new uint[1];
            hlayers[0] = 2;
            mlpNetwork = new Mlp(2, hlayers, 1, 1);
        }

        [TestMethod]
        public void TestMlpBuild()
        {
            mlpNetwork.Instantiate();

            Assert.AreEqual(mlpNetwork.CurrentError, 0);
            Assert.AreEqual(mlpNetwork.HiddenLayers, 1);
            Assert.AreEqual(mlpNetwork.TransferFunction, ActivationNeuroMode.SIGMOIDAL);
            Assert.AreEqual(mlpNetwork.OutputSize, 1);
            Assert.AreEqual(mlpNetwork.InputSize, 2);
        }

        [TestMethod]
        public void TestMlpTrainingPerformance()
        {

        }
    }
}
