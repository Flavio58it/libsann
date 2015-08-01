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
*	Kohonen map header file
*/

#ifndef KOHONENMAP_H
#define KOHONENMAP_H

#include "Global.h"
#include "Kneuron.h"

namespace libsann 
{
	// Kohonen map side limit 
	const int Lower_limit = 8;		// (lower) The Kohonen map will have at least 64 units
	const int Upper_limit = 46300;	// (upper) 

	// Kohonen map class
	class KohonenMap
	{
	public:
		KohonenMap(uint map_side_size);
		~KohonenMap();
		// Access methods
		vector<Kneuron*>* GetMapRow(uint index);
		inline uint GetNumberOfRows()			{ return map.size(); }
		Kneuron* GetNeuron(uint r, uint c);
		// Reset all synapse of all neurons
		void Reset(SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		inline uint GetNeighborhoodExtension()	{ return neighborhood_extension; }
		void SetNeighborhoodExtension(uint value);
		inline void ForceSetNeighborhoodExtension(uint value) // Force to set a extension of neighborhood
		{
			neighborhood_extension = value;
		}
		inline void RestoreNeighbohroodExtension()
		{
			neighborhood_extension = map.size() / 8;
		}
		// Return a matrix of output values
		vector<vector<double>>* GetOutputMatrix();
		vector<double> *KohonenToArray();
		// Find the winner neuron of the map 
		// at [0] = row, at [1] = column
		vector<uint>* FindWinnerNeuronCoordinates();
		Kneuron* FindWinnerNeuron();
		// Control utility
		bool UnitCoordinatesCoherence(int c, int r);
		// wrapper with a short name
		bool Ucc(int r, int c);
		// Normalize matrix
		void NormalizeMap();

	protected:
		// Neighborhood extension, 
		// number of neighbohrood units calculable with: extension size ^ 2
		uint neighborhood_extension;
		// Kohonen map neuron
		vector<vector<Kneuron*>*> map;
	};
}

#endif