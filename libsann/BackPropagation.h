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
*	Backpropagation header file
*/

#ifndef BACKPROPAGATION_H
#define BACKPROPAGATION_H

#include "Global.h"
#include "PropagationBase.h"

namespace libsann
{
	// Standard implementation of Backpropagation training algorithm
	class BackPropagation : public PropagationBase
	{
	public:
		BackPropagation(
			Mlp* net,
			double _errortarget = 0.01,
			uint _max_epochs = 100000,
			double _learning_rate = 0.2,
			double _beta = 0.2
			);
		~BackPropagation() { }

	protected:
		// Internal implementation of propagation algorithm
		virtual void OnLineLearning();
		virtual void BatchLearning();
		// Back propagation parameters
		double learningRate;
	};
}

#endif