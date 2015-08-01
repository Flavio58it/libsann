#include "InputLayer.h"

namespace libsann
{
	InputLayer::InputLayer(uint in_neurons)
	{
		LayerBase::LayerBase();
		for (uint i = 0; i < in_neurons; i++)
		{
			neurons.push_back(new InputNeuron());
		}
	}

	void InputLayer::SetInput(vector<double> data)
	{
		if (data.size() != neurons.size() - (this->BiasPresent == true) ? (1) : (0))
			throw(new exception("Input data size is not equal to number of input units."));

		for (uint i = 0; i < data.size(); i++)
		{
			if (((InputNeuron*)(neurons[i]))->IsBiasNeuron()) continue; // Skip the bias
			((InputNeuron*)(neurons[i]))->SetOutput(data[i]);
		}
	}
}