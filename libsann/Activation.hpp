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
*	Activation class collection for the neurons
*/

#ifndef ACTIVATION_HPP
#define ACTIVATION_HPP

#include "Global.h"

namespace libsann
{
	const double ME = 3.141592653589793238;

	enum ActivationMode { SIGMOIDAL, HYPERBOLIC_TANGENT, STEP, IDENTITY };

	// Abstract class for activation functions
	class activation
	{
	public:
		activation() {}
		~activation() {}

		virtual double TransFunction(double x) = 0;
		virtual double DerivativeFunction(double x) = 0;
	};

	// Sigmoid
	class Sigmoid : public activation
	{
	public:
		Sigmoid() {}
		~Sigmoid() {}

		// Sigmoidal function
		// f(x) = 1/(1 + e^-x)
		double TransFunction(double x)
		{
			return(1 / (1 + pow(ME, -x)));
		}

		// Sigmoidal derivative
		// f'(x) = x*(1-x)
		double DerivativeFunction(double x)
		{
			return(x*(1 - x));
		}
	};

	class hyperbolic_tangent : public activation
	{
	public:
		hyperbolic_tangent() {}
		~hyperbolic_tangent() {}

		// Hyperbolic tangent function
		// f(x) = ((e^x)-(e^-x)) / ((e^x)+(e^-x))
		double TransFunction(double x)
		{
			return((pow(ME, x) - pow(ME, -x)) / (pow(ME, x) + pow(ME, -x)));
		}

		// Hyperbolic tangent derivative
		// f'(x) = 1-x^2
		double DerivativeFunction(double x)
		{
			return(1 - pow(x, 2));
		}
	};

	class step : public activation
	{
	public:
		step() { }
		~step() { }

		double TransFunction(double x)
		{
			return((x < 0) ? (0) : (1));
		}
		double DerivativeFunction(double x)
		{
			return((x < 0) ? (0) : (1));
		}
	};

	class identity : public activation
	{
	public:
		identity() { }
		~identity() {  }

		double TransFunction(double x)
		{
			return(x);
		}
		double DerivativeFunction(double x)
		{
			return(1);
		}
	};
};

#endif