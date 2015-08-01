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
*	 Training set header file
*/

#ifndef TRAININGSET_H
#define TRAININGSET_H

#include "Global.h"

namespace libsann 
{
	class Pattern;

	// Class for the set of pattern
	class Trainingset
	{
	public:
		// Constructors
		Trainingset() { }
		Trainingset(vector<vector<double>> in, vector<vector<double>> out);
		Trainingset(double **matrix_in, double **matrix_out, uint rows, uint n_in, uint n_out);
		~Trainingset();

		void AddPattern(Pattern *p);
		void RemovePattern(uint index);
		uint size();
		inline Pattern* GetPatternAt(uint index) { return patternSet[index]; }

	protected:
		// Pattern set
		vector<Pattern*> patternSet;
	};
}

#endif

#include "Pattern.h"