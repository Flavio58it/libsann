#include "WindrowHoff.h"

namespace libsann 
{
	WindrowHoff::WindrowHoff(MAdeline *net, double _error_target, uint _max_epochs, double _learning_rate)
	{
		network = net;

		errorTarget = _error_target;
		maxEpochs = _max_epochs;
		learningRate = _learning_rate;
	}

	void WindrowHoff::training(SynInitMode mode, bool OnLine)
	{
		epoch = 0;
		double eErr = 0;
		double pErr = 0;

		network->Reset(mode);
		network->SetEpochsForTraining(epoch);

		for (;;)
		{
			for (uint i = 0; i < set->size(); i++)
			{
				network->SetInput(set->GetPatternAt(i)->GetDataInput());

				network->Exec();

				pErr = 0;
				for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
				{
					pErr += pow(set->GetPatternAt(i)->GetDataOutput()[j] - network->GetOutputNeuronAt(j)->Output(), 2);
				}
				pErr *= (1.0 / (double)network->GetOutputNeurons()->size());

				// Update error over the entire epoch
				eErr += pErr;

				for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
				{
					// Error of output
					network->GetOutputNeuronAt(j)->SetDelta(
						(set->GetPatternAt(i)->GetDataOutput()[j] - network->GetOutputNeuronAt(j)->Output()) * network->GetOutputNeuronAt(j)->Derivative()
						);
					// Compute derivative of the synapses
					for (uint k = 0; k < network->GetOutputNeuronAt(j)->GetInSyn()->size(); k++)
					{
						network->GetOutputNeuronAt(j)->GetSynapseAt(k)->ComputeDerivative(network->GetOutputNeuronAt(j)->GetDelta());
					}
				}

				if (!OnLine)
				{
					BatchLearning();
				}
				else
				{
					OnLineLearning();
					Adjust();
				}
			}
			// Compute and checks the error of the network (mse)		
			eErr *= (1.0 / (double)set->size());	// Mean square error over the entire trainig set

			currentError = eErr;

			if (eErr <= errorTarget)
			{
				network->SetEpochsForTraining(epoch);
				return;	// Training end, target error has been reached
			}

			// Check number of epochs
			if (maxEpochs <= ++epoch)
				return;

			// Batch
			if (!OnLine)
			{
				// Update the weights
				Adjust();
				// Set to 0 delta weights
				ResetDeltaWeights();
			}

			// else continue training
			eErr = 0;
		}
	}

	void WindrowHoff::OnLineLearning()
	{
		for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
		{
			for (uint k = 0; k < network->GetOutputNeuronAt(j)->GetInSyn()->size(); k++)
			{
				network->GetOutputNeuronAt(j)->GetSynapseAt(k)->SetUpdateWeight(
					learningRate * network->GetOutputNeuronAt(j)->GetSynapseAt(k)->GetDerivative()
					);
			}
		}
	}

	void WindrowHoff::BatchLearning()
	{
		for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
		{
			for (uint k = 0; k < network->GetOutputNeuronAt(j)->GetInSyn()->size(); k++)
			{
				network->GetOutputNeuronAt(j)->GetSynapseAt(k)->AddUpdateWeight(
					learningRate * network->GetOutputNeuronAt(j)->GetSynapseAt(k)->GetDerivative()
					);
			}
		}
	}

	void WindrowHoff::Adjust()
	{
		for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
		{
			for (uint k = 0; k < network->GetOutputNeuronAt(j)->GetInSyn()->size(); k++)
			{
				network->GetOutputNeuronAt(j)->GetSynapseAt(k)->Update();
			}
		}
	}

	void WindrowHoff::ResetDeltaWeights()
	{
		for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
		{
			for (uint k = 0; k < network->GetOutputNeuronAt(j)->GetInSyn()->size(); k++)
			{
				network->GetOutputNeuronAt(j)->GetSynapseAt(k)->SetUpdateWeight(0);
			}
		}
	}
}