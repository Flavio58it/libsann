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
*	 Windrow Hoff algorithm header file
*/

#ifndef WINDROWHOFF_H
#define WINDROWHOFF_H

#include "Trainingset.h"
#include "MAdeline.h"

namespace libsann  
{
	// Training algorithm for M-Adeline network, is a approximate steepest descent algorithm.
	class WindrowHoff
	{
	public:
		WindrowHoff(
			MAdeline *net,
			double _error_target = 0.01,
			uint _max_epochs = 100000,
			double _learning_rate = 0.25
			);
		~WindrowHoff() { }
		inline void SetTrainingSet(Trainingset* _set)	{ set = _set; }
		void training(SynInitMode mode = SynInitMode::FAN_IN, bool OnLine = true);
		inline double GetCurrentError() { return currentError; }
		inline uint GetEpoch() { return epoch; }

	protected:
		// M-Adeline pointer
		MAdeline *network;
		double errorTarget;
		uint maxEpochs;
		uint epoch;
		Trainingset *set;
		double learningRate;
		double currentError;
		// Internal implementation of propagation algorithm
		void OnLineLearning();
		void BatchLearning();
		void Adjust();
		void ResetDeltaWeights();
	};
}

#endif