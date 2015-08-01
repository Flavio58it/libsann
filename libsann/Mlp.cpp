#include "Mlp.h"

namespace libsann
{
	Mlp::Mlp(InputLayer* input, vector<FeedForwardLayer*> hiddens, FeedForwardLayer* output)
	{
		// Object componenets initialization
		inLayer = input;
		HiddenLayers = hiddens;
		outLayer = output;
		epochsForTraining = 0;

		if ((inLayer == NULL) || (outLayer == NULL) || (HiddenLayers.size() < 1))
			throw(new exception("Layers configuration not valid. Feed-forward networks must have at least one input, one hidden and one output layer."));
	}

	Mlp::~Mlp()
	{
		delete inLayer;
		for (uint i = 0; i < HiddenLayers.size(); i++)
			delete HiddenLayers[i];
		delete outLayer;
	}

	void Mlp::BuildNet(bool addBias, SynInitMode mode)
	{
		// Reset current connections
		for (uint i = 0; i < HiddenLayers.size(); i++)
		{
			HiddenLayers[i]->RemoveAllConnections();
		}
		outLayer->RemoveAllConnections();

		// Add bias if required
		if (addBias)
		{
			inLayer->CreateBias();
			for (uint i = 0; i < HiddenLayers.size(); i++) HiddenLayers[i]->CreateBias();
		}

		// Build new connections from input to first hidden layer
		for (uint i = 0; i < HiddenLayers[0]->GetNeurons()->size(); i++)
		{
			if (!(HiddenLayers[0]->GetNeuronAt(i)->IsBiasNeuron()))
			{
				for (uint j = 0; j < inLayer->GetNeurons()->size(); j++)
				{
					Synapse* s = new Synapse();
					s->SetWeight(0);
					s->ConnectPre(inLayer->GetNeuronAt(j));
					s->ConnectPost(HiddenLayers[0]->GetNeuronAt(i));

					HiddenLayers[0]->GetNeuronAt(i)->AddSynapse(s);
					inLayer->GetNeuronAt(j)->AddSynapseOut(s);
				}
			}
		}

		// Connects first hidden layer to input layer
		HiddenLayers[0]->SetPreviousLayerPtr(inLayer);
		if (HiddenLayers.size() == 1) HiddenLayers[0]->SetNextLayerPtr(outLayer);

		// Build new connections of hidden layers
		for (uint i = 1; i < HiddenLayers.size(); i++)
		{
			for (uint j = 0; j < HiddenLayers[i]->GetNeurons()->size(); j++)
			{
				if (!(HiddenLayers[i]->GetNeuronAt(j)->IsBiasNeuron()))
				{
					for (uint n = 0; n < HiddenLayers[i - 1]->GetNeurons()->size(); n++)
					{
						Synapse* s = new Synapse();
						s->SetWeight(0);
						s->ConnectPre(HiddenLayers[i - 1]->GetNeuronAt(n));
						s->ConnectPost(HiddenLayers[i]->GetNeuronAt(j));

						HiddenLayers[i]->GetNeuronAt(j)->AddSynapse(s);
						HiddenLayers[i - 1]->GetNeuronAt(n)->AddSynapseOut(s);
					}
				}
			}

			// Connects hidden layer with his next and previous layer
			HiddenLayers[i]->SetPreviousLayerPtr(HiddenLayers[i - 1]);
			HiddenLayers[i - 1]->SetNextLayerPtr(HiddenLayers[i]);

			if ((i + 1) >= HiddenLayers.size())
			{
				HiddenLayers[i]->SetNextLayerPtr(outLayer);
			}
		}

		// Build new connections from hidden layers to output layer
		for (uint i = 0; i < outLayer->GetNeurons()->size(); i++)
		{
			for (uint j = 0; j < HiddenLayers[HiddenLayers.size() - 1]->GetNeurons()->size(); j++)
			{
				Synapse* s = new Synapse();
				s->SetWeight(0);
				s->ConnectPre(HiddenLayers[HiddenLayers.size() - 1]->GetNeuronAt(j));
				s->ConnectPost(outLayer->GetNeuronAt(i));

				outLayer->GetNeuronAt(i)->AddSynapse(s);
				HiddenLayers[HiddenLayers.size() - 1]->GetNeuronAt(j)->AddSynapseOut(s);
			}
		}

		// Connects output layer to last hidden layer
		outLayer->SetPreviousLayerPtr(HiddenLayers[HiddenLayers.size() - 1]);

		Reset(mode);
	}

#ifdef USE_BOOST_LIB
	void Mlp::Mt_exec()
	{
		if ((inLayer == NULL) || (outLayer == NULL) || (HiddenLayers.size() <= 0))
			throw(new exception(
			"Layers configuration not valid. Multi-layer perceptron must have at least one input, one hidden and one output layer."
			));

		// Propagate throught hidden layers
		for (uint j = 0; j < HiddenLayers.size(); j++)
		{
			boost::thread_group threads;
			for (uint i = 0; i < HiddenLayers[j]->GetNeurons()->size(); i++)
			{
				threads.add_thread(new boost::thread(
					boost::bind(&Neuron::AcquiringPotential, HiddenLayers[j]->GetNeuronAt(i))
					));
			}
			threads.join_all();
		}

		// Propagate from hidden layers to output layer
		for (uint i = 0; i < outLayer->GetNeurons()->size(); i++)
		{
			outLayer->GetNeuronAt(i)->AcquiringPotential();
		}
	}
#endif

	void Mlp::Exec()
	{
		if ((inLayer == NULL) || (outLayer == NULL) || (HiddenLayers.size() <= 0))
			throw(new exception(
			"Layers configuration not valid. Multi-layer perceptron must have at least one input, one hidden and one output layer."
			));

		// Propagate throught hidden layers
		for (uint j = 0; j < HiddenLayers.size(); j++)
		{
			for (uint i = 0; i < HiddenLayers[j]->GetNeurons()->size(); i++)
			{
				HiddenLayers[j]->GetNeuronAt(i)->AcquiringPotential();
			}
		}

		// Propagate from hidden layers to output layer
		for (uint i = 0; i < outLayer->GetNeurons()->size(); i++)
		{
			outLayer->GetNeuronAt(i)->AcquiringPotential();
		}
	}

	void Mlp::Reset(SynInitMode mode)
	{
		double beta = 0;
		switch (mode)
		{
		case libsann::NGUYEN_WINDROW:
		{
										// 1 - Random initialization
										for (uint i = 0; i < HiddenLayers.size(); i++)
											HiddenLayers[i]->Reset(SynInitMode::POSITIVE_NEGATIVE_RANGE);
										outLayer->Reset(SynInitMode::POSITIVE_NEGATIVE_RANGE);

										// 2 - Apply Nguyen Windrow
										double h = 0;

										for (uint i = 0; i < HiddenLayers.size(); i++)
											h += HiddenLayers[i]->GetOnlyNeuronsAmount();

										beta = 0.7 * pow(h, 1.0 / inLayer->GetOnlyNeuronsAmount());

										outLayer->Reset(mode, beta);
										break;
		}
		case libsann::FAN_IN:
			if (outLayer->GetOnlyNeuronsAmount() < 2)
				outLayer->Reset();
			else
				outLayer->Reset(mode, beta);
		default:
			break;
		}

		for (uint i = 0; i < HiddenLayers.size(); i++)
			HiddenLayers[i]->Reset(mode, beta);
	}

	FeedForwardLayer* Mlp::GetHiddenLayerAt(uint index)
	{
		if (index >= HiddenLayers.size())
			throw(new exception("Hidden layer: The index required is out of range."));
		return HiddenLayers[index];
	}

	uint Mlp::SynapsesNumber()
	{
		uint hidden_syn = 0;
		for (uint h = 0; h < HiddenLayers.size(); h++)
		{
			hidden_syn += HiddenLayers[h]->GetTotalInSyn();
		}
		return(outLayer->GetTotalInSyn() + hidden_syn);
	}

	void Mlp::LoadSynapses(vector<double> v)
	{
		_ASSERT(v.size() == SynapsesNumber());
		uint c = 0;

		for (uint h = 0; h < HiddenLayers.size(); h++)
		{
			for (uint n = 0; n < HiddenLayers.at(h)->GetNeurons()->size(); n++)
			{
				for (uint s = 0; s < HiddenLayers.at(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
				{
					HiddenLayers.at(h)->GetNeuronAt(n)->GetSynapseAt(s)->SetWeight(v.at(c++));
				}
			}
		}
		for (uint n = 0; n < outLayer->GetNeurons()->size(); n++)
		{
			for (uint s = 0; s < outLayer->GetNeuronAt(n)->GetInSyn()->size(); s++)
			{
				outLayer->GetNeuronAt(n)->GetSynapseAt(s)->SetWeight(v.at(c++));
			}
		}
	}

	vector<double> Mlp::SaveSynapses()
	{
		vector<double> *v = new vector<double>();

		for (uint h = 0; h < HiddenLayers.size(); h++)
		{
			for (uint n = 0; n < HiddenLayers.at(h)->GetNeurons()->size(); n++)
			{
				for (uint s = 0; s < HiddenLayers.at(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
				{
					v->push_back(HiddenLayers.at(h)->GetNeuronAt(n)->GetSynapseAt(s)->GetWeight());
				}
			}
		}
		for (uint n = 0; n < outLayer->GetNeurons()->size(); n++)
		{
			for (uint s = 0; s < outLayer->GetNeuronAt(n)->GetInSyn()->size(); s++)
			{
				v->push_back(outLayer->GetNeuronAt(n)->GetSynapseAt(s)->GetWeight());
			}
		}
		return *v;
	}

	double* Mlp::SaveSynapsesToArray()
	{
		vector<double> v = SaveSynapses();
		double *weights = new double[v.size()];

		int i = 0;
		for each (double var in v)
		{
			weights[i++] = var;
		}

		return weights;
	}
}