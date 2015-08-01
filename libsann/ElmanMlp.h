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
*	Elman mlp header file
*/

#ifndef ELMANMLP_H
#define ELMANMLP_H

#include "FeedForwardLayer.h"
#include "InputLayer.h"
#include "ContextLayer.h"
#include "Mlp.h"
#include "Global.h"

namespace libsann
{
	// Elman network, Mlp with memory 
	// Elman, Jeffrey L.  (1990).  "Finding structure in time."  Cognitive Science, 14, pp. 179-211
	class ElmanMlp : public Mlp
	{
	public:
		ElmanMlp(InputLayer* input, vector<FeedForwardLayer*> hiddens, FeedForwardLayer* output);
		~ElmanMlp();

		void BuildNet(bool addBias = false, SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		void Exec();
		void Reset(SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);

	protected:
		// Context layers
		vector<ContextLayer*> ContextLayers;
	};
}

#endif