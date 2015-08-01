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
*	Network base header file
*/

#ifndef NETBASE_H
#define NETBASE_H

#include "Neuron.h"
#include "InputLayer.h"

namespace libsann 
{
	// Network interface
	class NetBase
	{
	public:
		NetBase() { }
		~NetBase() { }
		// Set input pattern to input neurons
		void SetInput(vector<double> data);
		// Reset memory of the network
		virtual void Reset(SynInitMode mode) = 0;
		// Network execution
		virtual void Exec() = 0;
		// Access methods
		vector<Neuron*>* GetOutputNeurons();
		vector<Neuron*>* GetInputNeurons();
		Neuron* GetOutputNeuronAt(uint index);
		// Access to layers
		inline LayerBase* GetOutLayer() { return outLayer; }
		inline InputLayer* GetInLayer() { return inLayer; }
		// Load / Save synapses
		virtual void LoadSynapses(vector<double> v);
		virtual vector<double> SaveSynapses();

		double* GetOutputValues();
		vector<double> GetOutputVector();

		virtual uint SynapsesNumber();
		inline uint GetEpochsForTraining() { return epochsForTraining; }
		inline void SetEpochsForTraining(uint value) { epochsForTraining = value; }

	protected:
		// Input layer
		InputLayer* inLayer;
		// Output layer
		LayerBase* outLayer;
		// Training epochs
		uint epochsForTraining;
	};
}

#endif