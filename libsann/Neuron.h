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
*	Neuron header file
*/

#ifndef NEURON_H
#define NEURON_H

#include "activation.hpp"
#include "Unit.h"

namespace libsann 
{
	// Neuron main class
	class Neuron : public Unit
	{
	public:
		Neuron();
		virtual inline double Output();
		virtual inline double Derivative();
		virtual void AcquiringPotential();
		virtual void AcquiringDeltaError();
		// Set activation function for the neuron shot
		void SetActivationMode(ActivationMode mode = SIGMOIDAL);
		inline bool IsBiasNeuron()				{ return IsBias; }
		inline void SetError(double value)		{ Error = value; }
		inline double GetError()				{ return Error; }

	protected:
		// Neuron potential
		double potential;
		// Output value
		double output;
		// Activation function
		activation* activation_function;
		// Bias flag
		bool IsBias;
		// Error, this is a weigthed sum of post-sinaptic neuron of outgoing synapses.
		// it is the error that has been propagated back.
		double Error;
	};
}

#endif