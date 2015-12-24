#include "Neuron.h"

namespace libsann
{
	Neuron::Neuron()
	{
		// Object components initialization
		IsBias = false;
		potential = 0;
		delta = 0;
		Error = 0;
		output = 0;
		activation_function = NULL;
	}

	inline double Neuron::Output()
	{
		return(output);
	}

	inline double Neuron::Derivative()
	{
		return(activation_function->DerivativeFunction(output));
	}

	void Neuron::AcquiringPotential()
	{
		double accumulator = 0;
		for (uint i = 0; i < inSynapses.size(); i++)
		{
			accumulator += inSynapses[i]->GetWeight() * inSynapses[i]->GetPreNeuron()->Output();
		}
		potential = accumulator;
		// Activation
		output = activation_function->TransFunction(potential);
	}

	void Neuron::AcquiringDeltaError()
	{
		double accumulator = 0;
		for (uint i = 0; i < outSynapses.size(); i++)
		{
			accumulator += (outSynapses[i]->GetWeight() * outSynapses[i]->GetPostNeuron()->GetDelta());
		}
		Error = accumulator;
	}

	void Neuron::SetActivationMode(ActivationMode mode)
	{
		switch (mode)
		{
		case SIGMOIDAL:
		{
						  activation_function = new Sigmoid();
						  break;
		}
		case HYPERBOLIC_TANGENT:
		{
								   activation_function = new hyperbolic_tangent();
								   break;
		}
		case STEP:
		{
					 activation_function = new step();
					 break;
		}
		case IDENTITY:
		{
						 activation_function = new identity();
						 break;
		}
		default:
		{
				   activation_function = new Sigmoid();
				   break;
		}
		};
	}
}