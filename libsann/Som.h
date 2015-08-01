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
*	 Self-organizing network header file
*/

#ifndef SOM_H
#define SOM_H

#include "Global.h"
#include "kneuron.h"
#include "InputLayer.h"

namespace libsann
{
	// Self-organizing network class
	// Kohonen, Teuvo (1982). "Self-Organized Formation of Topologically Correct Feature Maps". Biological Cybernetics 43 (1): 59–69.
	class Som
	{
	public:
		Som(uint input_size, uint map_side_size = 32);
		~Som();
		// Builds self-organizing network
		void BuildNet();
		// Network execution
		void Exec();
		// Set input pattern to input neurons
		void SetInput(vector<double> data);
		// Reset / initialize the synapses with a randomly values (-1;1)
		void Reset(SynInitMode mode = SynInitMode::POSITIVE_NEGATIVE_RANGE);
		// Wraps the map's methods
		Kneuron* WinnerNeuron();
		vector<uint>* WinnerNeuronCoordinates(); // at [0] = row, at [1] = column
		Kneuron* GetOutputNeuronAt(uint r, uint c);
		vector<vector<double>>* GetOutputMatrix();
		uint GetNeighbohroodExtension();
		void SetNeighbohroodExtension(uint value);
		inline void RestoreNeighbohroodExtension() { kmap->RestoreNeighbohroodExtension(); }
		bool Ucc(int r, int c);
		vector<double> *KohonenMapToArray();

	protected:
		// Kohonen map
		KohonenMap *kmap;
		InputLayer *InLayer;
	};
}

#endif