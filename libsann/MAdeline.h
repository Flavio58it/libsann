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
*	M-Adeline header file
*/

#ifndef MADELINE_H
#define MADELINE_H

#include "FeedForwardNetwork.h"
#include "FeedForwardLayer.h"

namespace libsann
{
	class InputLayer;

	// M-Adeline network is a perceptron with input neurons with identity function for activation
	class MAdeline : public FeedForwardNetwork
	{
	public:
		MAdeline(InputLayer* input, FeedForwardLayer* output);
		~MAdeline();

		void BuildNet(bool addBias = false, SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		void Reset(SynInitMode mode);
		void Exec();
	};
}

#include "InputLayer.h"
#endif
