using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace libsannNET
{
    public abstract class FeedForwardNetwork
    {
        #region [ Members ]

        /// <summary>
        /// Current error of the network
        /// </summary>
        protected double _currentError;
        /// <summary>
        /// Pointer to the network
        /// </summary>
        protected IntPtr _network;
        /// <summary>
        /// Number of input neurons
        /// </summary>
        protected uint _inputSize;
        /// <summary>
        /// Transfer function tyoe
        /// </summary>
        protected ActivationNeuroMode _transferFunction;
        /// <summary>
        /// Bias flag, true if use bias
        /// </summary>
        protected bool useBias;
        /// <summary>
        /// Number of neurons in the output layer
        /// </summary>
        protected uint _outputSize;

        #endregion

        #region [ Properties ]

        public double CurrentError
        {
            get { return _currentError; }
        }

        public uint InputSize
        {
            get { return _inputSize; }
        }

        public ActivationNeuroMode TransferFunction
        {
            get { return _transferFunction; }
        }

        public bool UseBias
        {
            get { return useBias; }
        }

        public uint OutputSize
        {
            get { return _outputSize; }
        }

        public uint EpochsTraining
        {
            get { return LibsannInvoke.GetEpoch(_network); }
        }

        #endregion

        #region [ Public abstract methods ]

        public abstract void Instantiate();

        public abstract void Destroy();

        #endregion

        #region [ Public methods ]

        /// <summary>
        /// Network execution, with 'pattern' inputs. Return the output values
        /// </summary>
        /// <param name="pattern">Input values</param>
        /// <returns>Network output values</returns>
        public virtual double[] Exec(double[] pattern)
        {
            // Set input
            LibsannInvoke.NetSetInput(_network, pattern, pattern.Count());
            // Execution
            IntPtr result = LibsannInvoke.NetExec(_network, _outputSize);
            // Pass output to unmanaged to managed array
            double[] ret = new double[_outputSize];
            Marshal.Copy(result, ret, 0, ret.Count());

            return ret;
        }

        #endregion
    }
}
