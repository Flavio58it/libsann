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
*	Pattern header file
*/

#ifndef PATTERN_H
#define PATTERN_H

#include "Global.h"

namespace libsann 
{
	// Class for single patter, The pattern contains the input vector and the output vector
	class Pattern
	{
	public:
		Pattern() { }
		Pattern(vector<double> in, vector<double> out);
		Pattern(double* in, double* out, uint in_count, uint out_count);
		~Pattern() { }
		inline vector<double> GetDataInput() { return dataInput; }
    inline double* GetDataInputAsArray() { return &dataInput[0]; }
		inline vector<double> GetDataOutput() { return dataOutput; }
		void SetData(vector<double> input, vector<double> output);

	protected:
		vector<double> dataInput;
		vector<double> dataOutput;
	};
}

#endif