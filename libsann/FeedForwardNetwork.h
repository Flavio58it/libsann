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
*	Feedforward network header file
*/

#ifndef FEEDFORWARDNETWORK_H
#define FEEDFORWARDNETWORK_H

#include "NetBase.h"
#include "Global.h"

namespace libsann
{
	// Feed-Forward is a family of ann, where input signal carries out through two or more levels of neurons,
	// in only one direction.
	class FeedForwardNetwork : public NetBase
	{
	public:
		virtual void Reset(SynInitMode mode) = 0;
		// Pure virtual function for network building (pure virtual)
		virtual void BuildNet(bool addBias, SynInitMode mode) = 0;
		virtual void Exec() = 0;
	};
}

#endif