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
*	Resilient propagation header file
*/

#ifndef RESILIENTPROPAGATION_H
#define RESILIENTPROPAGATION_H

#include "PropagationBase.h"

namespace libsann
{
	// Resilent propagation training algorithm
	class ResilientPropagation : public PropagationBase
	{
	public:
		ResilientPropagation(
			Mlp* net,
			double _errortarget = 0.01,
			uint _max_epochs = 100000,
			double _max_update_value = 50.0,
			double _min_update_value = 0.001,
			double _growth_factor = 1.2,
			double _decrease_factor = 0.5
			);
		~ResilientPropagation() { }

	protected:
		// Resilient propagation parameters
		double maxUpdateValue;
		double minUpdateValue;
		double growthFactor;
		double decreaseFactor;
		// Internal implementation of propagation algorithm
		virtual void OnLineLearning();
		virtual void BatchLearning();
		virtual void Adjust();
		// Core implementation
		void Rprop(Synapse *s);
	};
}

#endif