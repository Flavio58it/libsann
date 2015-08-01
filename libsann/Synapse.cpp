#include "synapse.h"

namespace libsann
{
  Synapse::Synapse()
	{
		// Object componenets initialization
		weight = 0;
		pre = NULL;
		post = NULL;
		update_weight = 0;
		UpdateValue = 0;
		derivative = 0;
		last_derivative = 0;
		last_update_weight = 0;
		gradients = 0;
		last_gradients = 0;
	}

	void Synapse::Reset()
	{
		weight = 0;
		update_weight = 0;
		UpdateValue = 0;
		derivative = 0;
		last_derivative = 0;
		last_update_weight = 0;
		gradients = 0;
		last_gradients = 0;
	}

  void Synapse::Update(bool inverted)
	{
		if (inverted)
		{
			weight -= update_weight;
		}
		else
		{
			weight += update_weight;
		}
	}

  void Synapse::ComputeDerivative(double neuron_delta)
	{
		last_derivative = derivative;
		derivative = neuron_delta * pre->Output();
	}

  void Synapse::SetUpdateWeight(double value)
	{
		last_update_weight = update_weight;
		update_weight = value;
	}

  void Synapse::AddUpdateWeight(double value)
	{
		update_weight += value;
	}
};