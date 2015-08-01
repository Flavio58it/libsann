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
*	 Synapse header file
*/

#ifndef SYNAPSE_H
#define SYNAPSE_H

#include "Global.h"

namespace libsann 
{
	class Unit;

	// Synapse class, connects two units of the network
	// Unit ---> synapse ---> Unit
	class Synapse
	{
	public:
		Synapse();
		~Synapse() { }
		// Reset synapse
		void Reset();
		// Connects the pre/post-synaptic neuron
		inline void ConnectPre(Unit* unit)		{ pre = unit; }
		inline Unit* GetPreNeuron()				{ return pre; }
		inline void ConnectPost(Unit* unit)		{ post = unit; }
		inline Unit* GetPostNeuron()			{ return post; }
		// Update
		void Update(bool inverted = false);
		inline void AddMomentum(double beta)	{ update_weight += (last_update_weight * beta); }
		// Weight access methods
		inline double GetWeight()				{ return weight; }
		inline void SetWeight(double value)		{ weight = value; }
		inline double GetUpdateWeight()			{ return update_weight; }
		void SetUpdateWeight(double value);
		void AddUpdateWeight(double value);
		// Gradients access methods
		inline void CleanGradients()			{ last_gradients = gradients; gradients = 0; }
		inline void  SetGradients(double value)	{ gradients = value; }
		inline double GetGradients()			{ return gradients; }
		inline double GetLastGradients()		{ return last_gradients; }
		// Update value access methods
		inline double GetUpdateValue()			{ return UpdateValue; }
		inline void SetUpdateValue(double value){ UpdateValue = value; }
		// Derivative access methods
		inline double GetDerivative()			{ return derivative; }
		void ComputeDerivative(double neuron_delta);
		inline double GetLastDerivative()		{ return last_derivative; }
		inline void AddDerivative(double value)	{ gradients += value; }

	private:
		// Weigh
		double weight;
		// Pre-synaptic unit pointer
		Unit* pre;
		// Post-synaptic unit pointer
		Unit* post;
		// Weight value
		double update_weight;
		double last_update_weight;
		// Update value
		double UpdateValue;
		// Derivatives
		double derivative;
		double last_derivative;
		// Gradients (comulative derivatives)
		double gradients;
		double last_gradients;
	};
}

#endif

#include "Unit.h"