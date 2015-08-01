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
*	Context layer header file
*/

#ifndef CONTEXTLAYER_H
#define CONTEXTLAYER_H

#include "LayerBase.h"
#include "ContextNeuron.h"

namespace libsann
{
	// Context layer is a memory for Mlp networks (Elman networks)
	class ContextLayer : public LayerBase
	{
	public:
		ContextLayer(int nn);
	};

}

#endif