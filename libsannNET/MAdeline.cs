using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace libsannNET
{
    public class MAdeline : FeedForwardNetwork
    {
        #region [ Constructors ]

        /// <summary>
        /// Build a new M-Adeline network
        /// </summary>
        /// <param name="inputUnits">Number of input neurons</param>
        /// <param name="outputUnits">Number of output neurons</param>
        /// <param name="bias">Bias flag (true raccomanded)</param>
        /// <param name="mode">Neurons activation mode</param>
        public MAdeline(uint inputUnits, uint outputUnits = 1, bool bias = true, ActivationNeuroMode mode = ActivationNeuroMode.SIGMOIDAL)
        {
            _inputSize = inputUnits;
            _outputSize = outputUnits;
            _transferFunction = mode;
            useBias = bias;
        }

        #endregion

        #region [ Public methods ]

        /// <summary>
        /// Instantiate structure of the network
        /// </summary>
        public override void Instantiate()
        {
            _network = LibsannInvoke.mAdelineInstantiate(_inputSize, _outputSize, useBias, _transferFunction);
        }

        /// <summary>
        /// Destroy internal network object
        /// </summary>
        public override void Destroy()
        {
            LibsannInvoke.mAdelineDelete(_network);
        }

        /// <summary>
        /// Training the network
        /// </summary>
        /// <param name="inputPatterns">Input patterns set</param>
        /// <param name="outputPatterns">Output patterns set</param>
        /// <param name="errorTarget">Target error</param>
        /// <param name="maxEpochs">Maximum number of epochs</param>
        /// <param name="mode">Weights initialization mode</param>
        /// <param name="learningRate">Learning rate</param>
        /// <returns>Returns true if the training is successful, otherwise it returns false</returns>
        public bool Training(
            double[][] inputPatterns,
            double[][] outputPatterns,
            double errorTarget = 0.01,
            uint maxEpochs = 100000,
            SynInitMode mode = SynInitMode.FAN_IN,
            double learningRate = 0.25
            )
        {
            IntPtr p_in;
            IntPtr p_out;

            // Conversion to unmanaged types
            LibsannInvoke.MatrixConversion(inputPatterns, out p_in);
            LibsannInvoke.MatrixConversion(outputPatterns, out p_out);

            _currentError = LibsannInvoke.mAdelineTrainingWithWindrowHoff(
                _network, p_in, p_out, (uint)inputPatterns.Count(), _inputSize, _outputSize, errorTarget, maxEpochs, learningRate, mode);

            if (errorTarget < _currentError)
                return false;
            else
                return true;
        }

        #endregion
    }
}
