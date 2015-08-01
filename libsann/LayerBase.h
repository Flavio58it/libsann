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
*	Layer base header file
*/

#ifndef LAYERBASE_H
#define LAYERBASE_H

#include "Bias.h"

namespace libsann 
{
	// Interface class for layers of the network
	class LayerBase
	{
	public:
		LayerBase();
		~LayerBase();
		// Insert a bias unit in the vector of neurons
		void CreateBias();
		// Edit neurons array
		void AddUnit(Neuron* u);
		void RemoveUnit(uint index);
		// Returns the pointer of vector of neurons pointers
		inline vector<Neuron*>* GetNeurons() { return &neurons; }
		// Get neuron pointer on 'index' position
		Neuron* GetNeuronAt(uint index);
		// Connects the units of this layer to another
		void ConnectTo(LayerBase* layer);
		// Create connections form another layer
		virtual void ConnectFrom(LayerBase* layer);
		// Removes all connections to this layer
		virtual void RemoveAllConnections();
		// Reset all synapse of all neurons
		void Reset(SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE, double beta = 0);
		inline bool BiasIsPresent() { return BiasPresent; }
		// Linked layer list access methods
		inline void SetPreviousLayerPtr(LayerBase* layer) { previous_layer = layer; }
		inline LayerBase* GetPreviousLayerPtr() { return previous_layer; }
		inline void SetNextLayerPtr(LayerBase* layer) { next_layer = layer; }
		inline LayerBase* GetNextLayerPtr() { return next_layer; }
		// Return total number of synapses
		uint GetTotalInSyn();
		// Get number of neurons except threshold
		uint GetOnlyNeuronsAmount();
    // Return the current output values of the neurons
    double *GetCurrentValues();

	protected:
		// Pointers to connected layers
		LayerBase* previous_layer;
		LayerBase* next_layer;
		// Neurons collection
		vector<Neuron*> neurons;
		bool BiasPresent;
	};
}

#endif