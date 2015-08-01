using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libsannNET
{
    public class ElmanMlp : Mlp
    {
        #region [ Constructors ]

        /// <summary>
        /// Build a new multi layered Elman feedforward network (mlp with memory)
        /// </summary>
        /// <param name="inputUnits">Number of input neurons</param>
        /// <param name="hiddenUnits">Number of hidden neurons for each hidden layers</param>
        /// <param name="hiddenLayers">Number of hidden layers</param>
        /// <param name="outputUnits">Number of output neurons</param>
        /// <param name="bias">Bias flag (true raccomanded)</param>
        /// <param name="mode">Neurons activation mode</param>
        public ElmanMlp(uint inputUnits, uint[] hiddenUnits, uint hiddenLayers, uint outputUnits, bool bias = true, ActivationNeuroMode mode = ActivationNeuroMode.SIGMOIDAL)
        {
            if (hiddenUnits.Count() != hiddenLayers)
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
        }

        #endregion 

        #region [ Public methods ]

        /// <summary>
        /// Instantiate structure of the network
        /// </summary>
        public override void Instantiate()
        {
            _network = LibsannInvoke.EMlpInstantiate(_inputSize, _hiddenLayers, _hiddenUnits, _outputSize, useBias, _transferFunction);
        }

        /// <summary>
        /// Destroy internal network object
        /// </summary>
        public override void Destroy()
        {
            LibsannInvoke.EMlpDelete(_network);
        }

        #endregion
    }
}