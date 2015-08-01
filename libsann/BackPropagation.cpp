/*/////////////////////////////////////////////////////////////////
*	libsann - Artificial neural networks library
*	Designed to be reliable and cross-platform.
*	C++ 11
*
*	Author: Matteo Tosato - tosatz{at}tiscali{dot}it
*	2013, 2014
*	Version: 0.3.1
////////////////////////////////////////////////////////////////*/

/*
*	Backpropagation implementation
*/

#include "BackPropagation.h"

namespace libsann
{
	BackPropagation::BackPropagation(Mlp* net, double _errortarget, uint _max_epochs, double _learning_rate, double _beta)
	{
		_ASSERT(net != NULL);

		// Object componenets initialization
		network = net;
		maxEpochs = _max_epochs;
		errorTarget = _errortarget;
		beta = _beta;
		learningRate = _learning_rate;
	}

	void BackPropagation::OnLineLearning()
	{
		for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
		{
			for (uint s = 0; s < network->GetOutputNeuronAt(o)->GetInSyn()->size(); s++)
			{
				network->GetOutputNeuronAt(o)->GetSynapseAt(s)->SetUpdateWeight(
					learningRate * network->GetOutputNeuronAt(o)->GetSynapseAt(s)->GetDerivative());
			}
		}
		for (uint h = 0; h < network->GetHiddenLayers()->size(); h++)
		{
			for (uint n = 0; n < network->GetHiddenLayerAt(h)->GetNeurons()->size(); n++)
			{
				if (!network->GetHiddenLayerAt(h)->GetNeuronAt(n)->IsBiasNeuron())
				{
					for (uint s = 0; s < network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
					{
						network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->SetUpdateWeight(
							learningRate * network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->GetDerivative());
					}
				}
			}
		}
	}

	void BackPropagation::BatchLearning()
	{
		for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
		{
			for (uint s = 0; s < network->GetOutputNeuronAt(o)->GetInSyn()->size(); s++)
			{
				network->GetOutputNeuronAt(o)->GetSynapseAt(s)->AddUpdateWeight(
					learningRate * network->GetOutputNeuronAt(o)->GetSynapseAt(s)->GetDerivative());
			}
		}
		for (uint h = 0; h < network->GetHiddenLayers()->size(); h++)
		{
			for (uint n = 0; n < network->GetHiddenLayerAt(h)->GetNeurons()->size(); n++)
			{
				if (!network->GetHiddenLayerAt(h)->GetNeuronAt(n)->IsBiasNeuron())
				{
					for (uint s = 0; s < network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
					{
						network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->AddUpdateWeight(
							learningRate * network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->GetDerivative());
					}
				}
			}
		}
	}
}