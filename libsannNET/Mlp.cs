using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace libsannNET
{
    public class Mlp : FeedForwardNetwork
    {
        #region [ Members ]
        /// <summary>
        /// Number of neurons for each hidden layers
        /// </summary>
        protected uint[] _hiddenUnits;
        /// <summary>
        /// Number of hidden layers
        /// </summary>
        protected uint _hiddenLayers;
        /// <summary>
        /// Training algorithm
        /// </summary>
        protected IntPtr trainAlg;
        /// <summary>
        /// Stats listener pointer
        /// </summary>
        protected Nullable<IntPtr> stats;
        /// <summary>
        /// Thread for training
        /// </summary>
        protected Thread trainingThread;
        #endregion

        #region [ Properties ]

        public uint[] HiddenUnits
        {
            get { return _hiddenUnits; }
        }

        public uint HiddenLayers
        {
            get { return _hiddenLayers; }
        }

        #endregion

        #region [ Constructors ]

        public Mlp() { }

        /// <summary>
        /// Build a new multi layered feedforward network
        /// </summary>
        /// <param name="inputUnits">Number of input neurons</param>
        /// <param name="hiddenUnits">Number of hidden neurons for each hidden layers</param>
        /// <param name="hiddenLayers">Number of hidden layers</param>
        /// <param name="outputUnits">Number of output neurons</param>
        /// <param name="bias">Bias flag (true raccomanded)</param>
        /// <param name="mode">Neurons activation mode</param>
        public Mlp(uint inputUnits, uint[] hiddenUnits, uint hiddenLayers, uint outputUnits, bool bias = true, ActivationNeuroMode mode = ActivationNeuroMode.SIGMOIDAL)
        {
            try
            {
                if(hiddenUnits.Count() != hiddenLayers)
                    throw (new ArgumentException("HiddenUnits inadequate for hiddenLayers."));
                else
                {
                    _outputSize = outputUnits;
                    _inputSize = inputUnits;
                    _hiddenUnits = hiddenUnits;
                    _hiddenLayers = hiddenLayers;
                    _transferFunction = mode;
                    useBias = bias;
                }
            } catch(Exception e) { throw e; }
        }

        #endregion

        #region [ Public methods ]

        /// <summary>
        /// Instantiate structure of the network
        /// </summary>
        public override void Instantiate()
        {
            try
            {
                _network = LibsannInvoke.MlpInstantiate(_inputSize, _hiddenLayers, _hiddenUnits, _outputSize, useBias, _transferFunction);
            } catch(Exception e) { throw e; }
        }

        /// <summary>
        /// Destroy internal network object
        /// </summary>
        public override void Destroy()
        {
            try
            {
                LibsannInvoke.MlpDelete(_network);
            } catch(Exception e) { throw e; }
        }

        public void InstantiateBackPropagationAlgorithm(
            double errorTarget = 0.01,
            uint maxEpochs = 100000,
            double learningRate = 0.2,
            double beta = 0.2)
        {
            trainAlg = LibsannInvoke.InstantiateBackPropagationTraininglgorithm(_network, errorTarget, maxEpochs, learningRate, beta);
        }

        public void InstantiateResilientPropagationAlgorithm(
            double errorTarget = 0.01,
            uint maxEpochs = 100000,
            double maxUpdateValue = 50.0,
            double minUpdateValue = 0.001,
            double growthFactor = 1.2,
            double decreaseFactor = 0.5
            )
        {
            trainAlg = LibsannInvoke.InstantiateResilientPropagationTrainingAlgorithm(_network, errorTarget, maxEpochs, maxUpdateValue, minUpdateValue, growthFactor, decreaseFactor);
        }

        /// <summary>
        /// Training the network
        /// </summary>
        /// <param name="inputPatterns">Input patterns set</param>
        /// <param name="outputPatterns">Output patterns set</param>
        /// <param name="errorTarget">Target error</param>
        /// <param name="mode">Initialization mode</param>
        /// <returns>Returns true if the training is successful, otherwise it returns false</returns>
        public bool Training(
            double[][] inputPatterns,
            double[][] outputPatterns,
            double errorTarget,
            SynInitMode mode = SynInitMode.NGUYEN_WINDROW
        )
        {
            IntPtr p_in;
            IntPtr p_out;

            try
            {
                if(trainAlg == null)
                    throw new Exception("Training algorithm not initialized.");

                if(stats != null)
                    LibsannInvoke.ResetPropStats(stats.Value);

                // Conversion to unmanaged types
                LibsannInvoke.MatrixConversion(inputPatterns, out p_in);
                LibsannInvoke.MatrixConversion(outputPatterns, out p_out);

                _currentError = LibsannInvoke.MlpTrainingWithPropagation(
                    _network, p_in, p_out, (uint)inputPatterns.Count(), _inputSize, _outputSize, trainAlg, mode);

                if(errorTarget < _currentError)
                    return false;
                else
                    return true;
            } catch(Exception e) { throw e; }
        }

        /// <summary>
        /// Training the network (Async call)
        /// </summary>
        /// <param name="inputPatterns">Input patterns set</param>
        /// <param name="outputPatterns">Output patterns set</param>
        /// <param name="errorTarget">Target error</param>
        /// <param name="mode">Initialization mode</param>
        /// <returns>Returns true if the training is successful, otherwise it returns false</returns>
        public async Task<bool> TrainingAsync(
            double[][] inputPatterns,
            double[][] outputPatterns,
            double errorTarget = 0.01,
            SynInitMode mode = SynInitMode.NGUYEN_WINDROW
        )
        {
            try
            {
                bool result = false;

                await Task.Run(() =>
                {
                    ThreadStart work = delegate
                    {
                        try
                        {
                            result = Training(inputPatterns, outputPatterns, errorTarget, mode);
                        } catch(Exception e) { throw e; }
                    };

                    Thread trainingThread = new Thread(work);
                    trainingThread.Start();
                    trainingThread.Join();
                });

                return result;

            } catch(Exception e) { throw e; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AbortTraining()
        {
            if(trainingThread != null && trainingThread.IsAlive)
                trainingThread.Abort();
        }

        /// <summary>
        /// Build internal stats listener 
        /// </summary>
        public void BuildStatsListener()
        {
            try
            {
                stats = LibsannInvoke.BuildStatsListener(trainAlg);
            } catch(Exception e) { throw e; }
        }

        /// <summary>
        /// Destroy the stats listener
        /// </summary>
        public void DestroyStatsListener()
        {
            try
            {
                LibsannInvoke.DestroyPropStatsListener(trainAlg);
                stats = null;
            } catch(Exception e) { throw e; }
        }

        /// <summary>
        /// Get all output values of the current training session
        /// </summary>
        /// <returns>Output values through time</returns>
        public List<double[][]> GetOutputValues()
        {
            try
            {
                if(stats == null)
                    throw new Exception("Stats listener not initialized.");

                int patterns_count;
                IntPtr outputs = LibsannInvoke.GetPropOutputValues(stats.Value, out patterns_count);

                int cols = Convert.ToInt32(_outputSize);

                List<double[][]> results;
                LibsannInvoke.MatrixConversion(outputs, patterns_count, 2, cols, out results);

                return results;

            } catch(Exception e) { throw e; }
        }

        /// <summary>
        /// Get error values through time
        /// </summary>
        /// <returns>Error values</returns>
        public double[] GetErrorValues()
        {
            try
            {
                if(stats == null)
                    throw new Exception("Stats listener not initialized.");

                int size;
                IntPtr errors = LibsannInvoke.GetPropErrorValue(stats.Value, out size);
                
                double[] results = new double[size];
                if(errors.ToInt32() != 0)
                {
                    Marshal.Copy(errors, results, 0, results.Count());
                }

                return results;
                
            } catch(Exception e) 
            { 
                throw e; 
            }
        }

        /// <summary>
        /// Get number of synapses of the network
        /// </summary>
        /// <returns>Weights values</returns>
        public double[] GetSynapsesWeights()
        {
            try
            {
                IntPtr result = LibsannInvoke.MlpGetWeights(_network);
                double[] ret = new double[LibsannInvoke.MlpGetWeightsNumber(_network)];
                Marshal.Copy(result, ret, 0, ret.Count());
                return ret;
            } catch(Exception e) 
            { 
                throw e;
            }
        }

        /// <summary>
        /// Set synapses weights
        /// </summary>
        /// <param name="vector">Weights values</param>
        public void SetSynapsesWeights(List<double> vector)
        {
            try
            {
                LibsannInvoke.MlpLoadWeights(_network, vector.ToArray(), vector.Count);
            } catch(Exception e) 
            { 
                throw e; 
            }
        }

        #endregion
    }
}
