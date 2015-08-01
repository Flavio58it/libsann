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
*	Elman network implementation
*/

#include "ElmanMlp.h"

namespace libsann 
{
	ElmanMlp::ElmanMlp(InputLayer* input, vector<FeedForwardLayer*> hiddens, FeedForwardLayer* output)
	{
		// Object componenets initialization
		inLayer = input;
		HiddenLayers = hiddens;
		outLayer = output;
		epochsForTraining = 0;

		if ((inLayer == NULL) || (outLayer == NULL) || (HiddenLayers.size() < 1))
			throw(new exception("Layers configuration not valid. Feed-forward networks must have at least one input, one hidden and one output layer."));

		// Build the context layers, the memory of the network
		for (uint i = 0; i < hiddens.size(); i++)
		{
			ContextLayers.push_back(new ContextLayer(hiddens.at(i)->GetNeurons()->size()));
		}
	}

	ElmanMlp::~ElmanMlp()
	{
		for (uint i = 0; i < ContextLayers.size(); i++)
			delete ContextLayers.at(i);
	}

	void ElmanMlp::BuildNet(bool addBias, SynInitMode mode)
	{
		// Connects all to all
		Mlp::BuildNet(addBias, SynInitMode::NONE);

		// Connects context layers to the hidden layers
		for (uint i = 0; i < HiddenLayers.size(); i++)
		{
			for (uint j = 0; j < HiddenLayers.at(i)->GetOnlyNeuronsAmount(); j++)
			{
				for (uint c = 0; c < ContextLayers.at(i)->GetNeurons()->size(); c++)
				{
					Synapse* s = new Synapse();
					s->SetWeight(0);
					s->ConnectPre(ContextLayers.at(i)->GetNeuronAt(c));
					s->ConnectPost(HiddenLayers.at(i)->GetNeuronAt(j));

					HiddenLayers.at(i)->GetNeuronAt(j)->AddSynapse(s);
					ContextLayers.at(i)->GetNeuronAt(c)->AddSynapseOut(s);
				}
			}
		}

		// Initialize
		Reset(mode);
	}

	void ElmanMlp::Reset(SynInitMode mode)
	{
		Mlp::Reset(mode);

		for (uint i = 0; i < HiddenLayers.size(); i++)
		{
			for (uint n = 0; n < HiddenLayers.at(i)->GetOnlyNeuronsAmount(); n++)
			{
				((ContextNeuron*)ContextLayers.at(i)->GetNeuronAt(n))->AcquiringPotential(0.5);
			}
		}
	}

	void ElmanMlp::Exec()
	{
		Mlp::Exec();

		// Store to context layer
		for (uint i = 0; i < HiddenLayers.size(); i++)
		{
			for (uint n = 0; n < HiddenLayers.at(i)->GetOnlyNeuronsAmount(); n++)
			{
				((ContextNeuron*)ContextLayers.at(i)->GetNeuronAt(n))->AcquiringPotential(HiddenLayers.at(i)->GetNeuronAt(n)->Output());
			}
		}
	}
}
