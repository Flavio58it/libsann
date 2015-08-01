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
*	Propagation network header file
*/

#ifndef PROPAGATIONBASE_H
#define PROPAGATIONBASE_H

#include "Global.h"
#include "Trainingset.h"
#include "PropStatsListener.h"

namespace libsann
{
	class Mlp;

	// Base class for propagation training family algorithms
	class PropagationBase
	{
	public:
		// Training on line mode, complete training process
		virtual void Training(SynInitMode mode, bool Online = true, bool Mt = false);
		// Set the training set
		void SetTrainingSet(Trainingset* _set);
		// Debug vectors
		vector<double> NetErrors;
		// Get current error
		inline double GetCurrentError() { return currentError; }
		// Get current epoch
		inline uint GetEpoch() { return epoch; }
		// Events 
		__event void CurrentValues(double*** values, int size);
		__event void CurrentErrorValue(double value);
		// Statistics listener
		PropStatsListener* BuildStatsListener();
		void DestroyStatsListener();

	protected:
		// Real-time parameters
		double currentError;
		uint epoch;
		Trainingset *set;
		// Training configuration
		uint maxEpochs;
		double errorTarget;
		// Listener for stats
		PropStatsListener* listener;
		// Training step
		void TrainingStep(Pattern* pattern);
		// Apply weights correction
		virtual void Adjust();
		// Virtual function of core algorithm implementation
		virtual void OnLineLearning() = 0;
		virtual void BatchLearning() = 0;
		// Reset delta weights (for batch learning)
		virtual void CleanUpdateWeights();

		// The network
		Mlp* network;
		// Momentum coefficient
		double beta;
	};
}

#endif

#include "Mlp.h"