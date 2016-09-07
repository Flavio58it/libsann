using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace libsannUnitTest
{
    using libsannNET;

    [TestClass]
    public class MlpTest
    {
        double[][] parityInput =
            {
                new [] {0.0,0.0,0.0},
                new [] {0.0,0.0,1.0},
                new [] {0.0,1.0,0.0},
                new [] {0.0,1.0,1.0},
                new [] {1.0,0.0,0.0},
                new [] {1.0,0.0,1.0},
                new [] {1.0,1.0,0.0},
                new [] {1.0,1.0,1.0}
            };
        double[][] parityOutput =
            {
                new [] {1.0},
                new [] {0.0},
                new [] {0.0},
                new [] {1.0},
                new [] {0.0},
                new [] {1.0},
                new [] {1.0},
                new [] {0.0}
            };
        double[][] parityInputValidation =
            {
                new [] {0.01,0.0,0.05},
                new [] {0.0,0.08,0.9},
                new [] {0.08,1.0,0.07},
                new [] {0.054,0.95,0.95},
                new [] {1.0,0.0,0.06},
                new [] {0.9,0.0,0.94},
                new [] {0.99,1.0,0.09},
                new [] {0.92,0.9,0.95}
            };

        Mlp mlpNetwork;
        StreamWriter wr;

        private void Build(uint input, uint hiddenLayers, uint[] hiddenNeurons, uint output)
        {
            // Build network model
            mlpNetwork = new Mlp(input, hiddenNeurons, hiddenLayers, output);
            mlpNetwork.Instantiate();
        }

        public MlpTest()
        {
            using(wr = new StreamWriter(@"test.log", true, System.Text.Encoding.ASCII, 1024))
            {
                wr.WriteLine(Environment.NewLine + DateTime.Now.ToShortDateString());
                wr.WriteLine("-------------------------");

                // Library init
                libsannNET.LibsannInvoke.libsannInit();
            }
        }

        [TestMethod]
        public void TestMlpBuild()
        {
            using(wr = new StreamWriter(@"test.log", true, System.Text.Encoding.ASCII, 1024))
            {
                // Build network model
                uint[] hlayers = new uint[3];
                hlayers[0] = 5;
                hlayers[1] = 4;
                hlayers[1] = 3;

                Build(10, 3, hlayers, 1);

                wr.WriteLine("-> Build network model and check it");

                Assert.AreEqual(mlpNetwork.CurrentError, 0);
                Assert.AreEqual(mlpNetwork.HiddenLayers, (uint)3);
                Assert.AreEqual(mlpNetwork.TransferFunction, ActivationNeuroMode.SIGMOIDAL);
                Assert.AreEqual(mlpNetwork.OutputSize, (uint)1);
                Assert.AreEqual(mlpNetwork.InputSize, (uint)10);
            }
        }

        [TestMethod]
        public void TestMlpTrainingWthResilientPropPerformance()
        {
            using(wr = new StreamWriter(@"test.log", true, System.Text.Encoding.ASCII, 1024))
            {
                try
                {
                    // Build network model
                    uint[] hlayers = new uint[1];
                    hlayers[0] = 4;

                    Build(3, 1, hlayers, 1);

                    wr.WriteLine("-> Test network performance for parity problem with rprop");

                    double targetError = 0.05;
                    uint epochs = 10000;

                    mlpNetwork.InstantiateResilientPropagationAlgorithm(targetError, epochs);
                    mlpNetwork.BuildStatsListener();

                    Task<bool> trainResult = mlpNetwork.TrainingAsync(parityInput, parityOutput, targetError);
                    DateTime start = DateTime.Now;

                    while(!trainResult.IsCompleted)
                    {
                        List<double[][]> output = mlpNetwork.GetOutputValues();
                        //wr.WriteLine("[" + (DateTime.Now-start).Milliseconds.ToString() + " ms] Output {0}", string.Join(";", output));
                        Thread.Sleep(2);
                    }

                    if(!trainResult.Result)
                        wr.WriteLine("-> Network training fail");
                    else
                    {
                        wr.WriteLine("-> Network trained! [Error: {0}, Epochs: {1}]", mlpNetwork.CurrentError, mlpNetwork.EpochsTraining);
                        wr.WriteLine("-> Validation:");

                        int i = 0;
                        foreach(double[] pattern in parityInputValidation)
                        {
                            double[] output = mlpNetwork.Exec(pattern);
                            wr.WriteLine("-> Input: " + pattern[0] + " " + pattern[1]);
                            wr.WriteLine("-> Output: " + output[0] + " (ideal: " + parityOutput[i++][0] + ")");
                        }
                        wr.WriteLine("-> Validation end");
                    }
                } catch(Exception e)
                {
                    wr.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
                }
            }
        }
    }
}