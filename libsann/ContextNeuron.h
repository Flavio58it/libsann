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
*	Context neuron header file
*/

#ifndef CONTEXTNEURON_H
#define CONTEXTNEURON_H

#include "Neuron.h"

namespace libsann
{
	// Neuron of the context layer. His potential is equal to hidden neuron output associated.
	// The activation mode is IDENTITY in order to simulate a weight with a value set to 1.0
	class ContextNeuron : public Neuron
	{
	public:
		ContextNeuron();

		void AcquiringPotential(double value);
		inline void AcquiringDeltaError()	{ }				// Nothing to do
	};
}

#endif