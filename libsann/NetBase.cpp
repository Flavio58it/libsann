#include "NetBase.h"

namespace libsann 
{
	void NetBase::SetInput(vector<double> data)
	{
		inLayer->SetInput(data);
	}

	vector<Neuron*>* NetBase::GetOutputNeurons()
	{
		return outLayer->GetNeurons();
	}

	vector<Neuron*>* NetBase::GetInputNeurons()
	{
		return inLayer->GetNeurons();
	}

	Neuron* NetBase::GetOutputNeuronAt(uint index)
	{
		return outLayer->GetNeuronAt(index);
	}

	uint NetBase::SynapsesNumber()
	{
		return outLayer->GetTotalInSyn();
	}

	void NetBase::LoadSynapses(vector<double> v)
	{
		_ASSERT(v.size() == SynapsesNumber());
		uint c = 0;

		for (uint n = 0; n < outLayer->GetNeurons()->size(); n++)
		{
			for (uint s = 0; s < outLayer->GetNeuronAt(n)->GetInSyn()->size(); s++)
			{
				outLayer->GetNeuronAt(n)->GetSynapseAt(s)->SetWeight(v.at(c++));
			}
		}
	}

	vector<double> NetBase::SaveSynapses()
	{
		vector<double> *v = new vector<double>();

		for (uint n = 0; n < outLayer->GetNeurons()->size(); n++)
		{
			for (uint s = 0; s < outLayer->GetNeuronAt(n)->GetInSyn()->size(); s++)
			{
				v->push_back(outLayer->GetNeuronAt(n)->GetSynapseAt(s)->GetWeight());
			}
		}
		return *v;
	}

	double* NetBase::GetOutputValues()
	{
    return outLayer->GetCurrentValues();
	}

	vector<double> NetBase::GetOutputVector()
	{
		vector<double> results;
		for (uint i = 0; i < outLayer->GetNeurons()->size(); i++)
		{
			results.push_back(outLayer->GetNeuronAt(i)->Output());
		}
		return results;
	}
}