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
*	 Self-organizing network learning header file
*/

#ifndef SOMLEARNING_H
#define SOMLEARNING_H

#include "Som.h"

namespace libsann
{
	// Competitive learning of self-organizing network.
	class SomLearning
	{
	public:
		SomLearning(Som* net, double _learning_rate = 0.25);
		~SomLearning() { }
		void SetDataSet(vector<vector<double>> *_set);
		// Unsupervised learning
		void Learning(uint maxEpochs, SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		// A vector of values about maximum weight update of last epoch
		inline vector<double> GetUpdateHistory()
		{
			return updateHistory;
		}

	protected:
		// Self-organinzing network pointer
		Som *network;
		// Learning rate
		double learningRate;
		// Data set
		vector<vector<double>> *set;
		//Decay factors
		double lrateDecayFact;
		double neighDecayFact;
		// Update history
		vector<double> updateHistory;
	};
}

#endif