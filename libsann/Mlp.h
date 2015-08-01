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
*	Multi-layer perceptron network header file
*/

#ifndef MLP_H
#define MLP_H

#include "FeedForwardNetwork.h"
#include "FeedForwardLayer.h"
#include "InputLayer.h"

namespace libsann
{
	// Multi-layer perceptron network, is the most used feedforward network structure for patterns classification
	// Rumelhart, David E., Geoffrey E. Hinton, and R. J. Williams. “Learning Internal Representations by Error Propagation”.
	// David E. Rumelhart, James L. McClelland, and the PDP research group. (editors), Parallel distributed processing: Explorations in the microstructure of cognition, Volume 1: Foundations. MIT Press, 1986.
	class Mlp : public FeedForwardNetwork
	{
	public:
		Mlp() { }
		Mlp(InputLayer* input, vector<FeedForwardLayer*> hiddens, FeedForwardLayer* output);
		Mlp::~Mlp();

		virtual void BuildNet(bool addBias = false, SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		virtual void Exec();
#ifdef USE_BOOST
		void Mt_exec(); // Multi-threading support
#endif
		virtual void Reset(SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		// Access to hidden layers
		inline vector<FeedForwardLayer*>* GetHiddenLayers() { return &HiddenLayers; }
		FeedForwardLayer* GetHiddenLayerAt(uint index);

		void LoadSynapses(vector<double> v);
		vector<double> SaveSynapses();
		double* SaveSynapsesToArray();

		// Properties
		uint SynapsesNumber();

	protected:
		// vector of hidden layers
		vector<FeedForwardLayer*> HiddenLayers;
	};
}

#endif