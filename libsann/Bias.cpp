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
*	Bias neuron implementation
*/

#include "Bias.h"

namespace libsann 
{
	Bias::Bias()
	{
		Neuron::Neuron();
		IsBias = true;
	}
}