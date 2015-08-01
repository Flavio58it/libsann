#include "ResilientPropagation.h"

namespace libsann
{
	ResilientPropagation::ResilientPropagation(
		Mlp* net,
		double _errortarget,
		uint _max_epochs,
		double _max_update_value,
		double _min_update_value,
		double _growth_factor,
		double _decrease_factor)
	{
		if (net != NULL) network = net;

		// Object componenets initialization
		network = net;
		maxEpochs = _max_epochs;
		errorTarget = _errortarget;
		maxUpdateValue = _max_update_value;
		minUpdateValue = _min_update_value;
		growthFactor = _growth_factor;
		decreaseFactor = _decrease_factor;
		beta = 0; // Not used 
	}

	void ResilientPropagation::OnLineLearning()
	{
		throw(new exception("Resilient propagation not support online mode."));
	}

	void ResilientPropagation::BatchLearning()
	{
		for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
		{
			for (uint s = 0; s < network->GetOutLayer()->GetNeuronAt(o)->GetInSyn()->size(); s++)
			{
				network->GetOutLayer()->GetNeuronAt(o)->GetSynapseAt(s)->AddDerivative(
					network->GetOutLayer()->GetNeuronAt(o)->GetSynapseAt(s)->GetDerivative());
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
						network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->AddDerivative(
							network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->GetDerivative());
					}
				}
			}
		}
	}

	void ResilientPropagation::Adjust()
	{
		// Run RPROP
		for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
		{
			for (uint s = 0; s < network->GetOutLayer()->GetNeuronAt(o)->GetInSyn()->size(); s++)
			{
				Rprop(network->GetOutLayer()->GetNeuronAt(o)->GetSynapseAt(s));
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
						Rprop(network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s));
					}
				}
			}
		}
	}

	void ResilientPropagation::Rprop(Synapse *s)
	{
		double updateValue = s->GetUpdateValue();
		double p = s->GetGradients() * s->GetLastGradients();
		double grad = s->GetGradients();

		if (p > 0)
		{
			updateValue = MIN(updateValue * growthFactor, maxUpdateValue);
			s->SetUpdateWeight(((grad > 0) ? (1.0) : (-1.0)) * updateValue);
			s->Update();
		}
		else if (p < 0)
		{
			updateValue = MAX(updateValue * decreaseFactor, minUpdateValue);
			s->Update(true);
			s->SetGradients(0);
		}
		else
		{
			s->SetUpdateWeight(((grad > 0) ? (1.0) : (-1.0)) * updateValue);
			s->Update();
		}
		s->SetUpdateValue(updateValue);
	}
}