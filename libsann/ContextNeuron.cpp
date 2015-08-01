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
*	Context neuron implementation
*/

#include "ContextNeuron.h"

namespace libsann
{
	ContextNeuron::ContextNeuron()
	{
		Neuron::Neuron();
		SetActivationMode(ActivationMode::IDENTITY);
	}

	void ContextNeuron::AcquiringPotential(double value)
	{
		potential = value;
	}
}