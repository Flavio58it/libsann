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
*	Kohonen neuron header file
*/

#ifndef KNEURON_H
#define KNEURON_H

#include "Neuron.h"

namespace libsann
{
  class KohonenMap;

	// Neuron of the Kohonen map
	class Kneuron : public Neuron
	{
	public:
		Kneuron();
		~Kneuron() { }
		// Implements the 'Euclidean distance' from input to weight
		void AcquiringPotential();
		// Simulated mutual elicitation between neighboring neurons
		void AcquiringNeighborhoodElicitation(int r, int c, uint ne_ext, KohonenMap* kmap_ptr);
		// Apply elicitation of neighboring neurons to potential
		void ApplyNeighborhoodElicitation();
		// Forze output value
		inline void SetOutput(double value)
		{
			output = value;
		}

	protected:
		// Influence of neighborhood
		double NeighborhoodElicitation;
	};
}

#endif

#include "KohonenMap.h"