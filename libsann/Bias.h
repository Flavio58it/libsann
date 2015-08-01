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
*	Bias header file
*/

#ifndef BIAS_H
#define BIAS_H

#include "Neuron.h"

namespace libsann 
{
	// Bias class
	class Bias : public Neuron
	{
	public:
		Bias();
		// Overrides
		inline double Output() { return 1.0; }	    // the bias return 1.0 as output value
		inline void AcquiringPotential()	{ }				// Nothing to do, can not have synapses
		inline void AcquiringDeltaError()	{ }				// Nothing to do
	};
}

#endif