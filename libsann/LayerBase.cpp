#include "LayerBase.h"

namespace libsann
{
	LayerBase::LayerBase()
	{
		// Object components initialization
		BiasPresent = false;
		previous_layer = NULL;
		next_layer = NULL;
	}

	LayerBase::~LayerBase()
	{
		for (uint i = 0; i < neurons.size(); i++)
			delete neurons[i];
	}

	void LayerBase::ConnectTo(LayerBase* layer)
	{
		for (uint i = 0; i < layer->neurons.size(); i++)
		{
			for (uint n = 0; n < neurons.size(); n++)
			{
				Synapse *syn = new Synapse();
				syn->ConnectPre(neurons[n]);
				syn->SetWeight(0);
				layer->GetNeuronAt(i)->AddSynapse(syn);
			}
		}
	}

	void LayerBase::ConnectFrom(LayerBase* layer)
	{
		for (uint i = 0; i < neurons.size(); i++)
		{
			for (uint n = 0; n < layer->GetNeurons()->size(); n++)
			{
				Synapse *syn = new Synapse();
				syn->ConnectPre(layer->GetNeuronAt(i));
				syn->SetWeight(0);
				neurons[i]->AddSynapse(syn);
			}
		}
	}

	void LayerBase::RemoveAllConnections()
	{
		for (uint i = 0; i < neurons.size(); i++)
		{
			neurons[i]->RemoveAllSynapses();
		}
		previous_layer = NULL;
		next_layer = NULL;
	}

	void LayerBase::CreateBias()
	{
		neurons.push_back(new Bias());
		BiasPresent = true;
	}

	void LayerBase::Reset(SynInitMode mode, double beta)
	{
		double alfa = 0;

		switch (mode)
		{
		case libsann::NGUYEN_WINDROW:
		{
										for (uint i = 0; i < neurons.size(); i++)
										{
											for (uint j = 0; j < neurons[i]->GetInSyn()->size(); j++)
											{
												if (((Neuron*)neurons[i]->GetSynapseAt(j)->GetPreNeuron())->IsBiasNeuron())
													continue; // Is the threshold
												alfa += pow(neurons[i]->GetInSyn()->at(j)->GetWeight(), 2.0);
											}
										}
										alfa = sqrt(alfa);		   // alfa is the Euclidean norm
										break;
		}
		case libsann::FAN_IN:
		{
								alfa = GetOnlyNeuronsAmount();
		}
		default:
			break;
		}

		for (uint i = 0; i < neurons.size(); i++)
		{
			neurons[i]->ResetAllSynapses(mode, alfa, beta);
		}
	}

	inline Neuron* LayerBase::GetNeuronAt(uint index)
	{
		if (index >= neurons.size())
			throw(new exception("Output neurons vector: The index required is out of range."));
		return neurons[index];
	}

	void LayerBase::AddUnit(Neuron *u)
	{
		neurons.push_back(u);
	}

	void LayerBase::RemoveUnit(uint index)
	{
		_ASSERT(index < neurons.size());
		if (neurons[index]->IsBiasNeuron()) BiasPresent = false;
		vector<Neuron*>::iterator i = neurons.begin();
		neurons.erase(i + index);
	}

	uint LayerBase::GetTotalInSyn()
	{
		uint syn_count = 0;
		for (uint n = 0; n < neurons.size(); n++)
		{
			syn_count += neurons.at(n)->GetInSyn()->size();
		}
		return syn_count;
	}

	uint LayerBase::GetOnlyNeuronsAmount()
	{
		for (uint i = 0; i < neurons.size(); i++)
		{
			if (neurons[i]->IsBiasNeuron())
				return neurons.size() - 1;
		}
		return neurons.size();
	}

  double* LayerBase::GetCurrentValues()
  {
    double *vec = new double[neurons.size()];
    for (int i = 0; i < neurons.size(); i++)
    {
      vec[i] = neurons.at(i)->Output();
    }
    return vec;
  }
}