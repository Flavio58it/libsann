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
*	Feedforward layer header file
*/

#ifndef FEEDFORWARDLAYER_H
#define FEEDFORWARDLAYER_H

#include "activation.hpp"
#include "LayerBase.h"

namespace libsann 
{
	// Feed-forward layer
	class FeedForwardLayer : public LayerBase
	{
	public:
		FeedForwardLayer(int nn, ActivationMode mode = SIGMOIDAL);
	};
}

#endif