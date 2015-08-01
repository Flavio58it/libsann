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
*	Input neuron header file
*/

#ifndef INPUTNEURON_H
#define INPUTNEURON_H

#include "InputNeuron.h"

namespace libsann 
{
	// Input neuron class, Input neuron has a fixed output value, 
	// takes it from the input pattern.
	class InputNeuron : public Neuron
	{
	public:
		InputNeuron() { Neuron::Neuron(); }
		inline double Output()				{ return OutputValue; }
		inline void SetOutput(double value)	{ OutputValue = value; }

	protected:
		double OutputValue;
	};
}

#endif