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
*	Input layer header file
*/

#ifndef INPUTLAYER_H
#define INPUTLAYER_H

#include "Global.h"
#include "LayerBase.h"

namespace libsann 
{
	// Input layer, is the first layer of the network. Input can be set here
	class InputLayer : public LayerBase
	{
	public:
		InputLayer(uint in_neurons);
		// Set values to the input of the network
		void SetInput(vector<double> data);
	};
}

#include "InputNeuron.h"

#endif
