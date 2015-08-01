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
*	Context layer implementation
*/

#include "ContextLayer.h"

namespace libsann
{
	ContextLayer::ContextLayer(int nn)
	{
		LayerBase::LayerBase();
		for (int i = 0; i < nn; i++)
		{
			ContextNeuron *n = new ContextNeuron();
			neurons.push_back(n);
		}
	}
}