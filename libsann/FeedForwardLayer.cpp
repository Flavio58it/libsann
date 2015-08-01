#include "FeedForwardLayer.h"

namespace libsann
{
	FeedForwardLayer::FeedForwardLayer(int nn, ActivationMode mode)
	{
		LayerBase::LayerBase();
		for (int i = 0; i < nn; i++)
		{
			Neuron *n = new Neuron();
			n->SetActivationMode(mode);
			neurons.push_back(n);
		}
	}
}
