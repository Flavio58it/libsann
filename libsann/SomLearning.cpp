#include "SomLearning.h"

namespace libsann
{
	SomLearning::SomLearning(Som* net, double _learning_rate)
	{
		network = net;
		learningRate = _learning_rate;
		set = NULL;
	}

	void SomLearning::SetDataSet(vector<vector<double>> *_set)
	{
		set = _set;
	}

	void SomLearning::Learning(uint maxEpochs, SynInitMode mode)
	{
		_ASSERT(set != NULL);

		double local_NeighbohroodExtension = (double)network->GetNeighbohroodExtension();
		double orig_lrate = learningRate;

		updateHistory.clear();

		neighDecayFact = local_NeighbohroodExtension / ((double)maxEpochs);
		lrateDecayFact = learningRate / maxEpochs;

		// Reset network knowledge
		network->Reset(mode);

		double update;

		for (uint e = 0; e < maxEpochs; e++)
		{
			update = 0;

			for (uint p = 0; p < set->size(); p++)
			{
				// Set pattern
				network->SetInput(set->at(p));

				// Exec
				network->Exec();

				// Get winner neuron of the competition
				vector<uint> *winPoints = network->WinnerNeuronCoordinates();
				uint column = winPoints->at(1);
				uint row = winPoints->at(0);
				uint ne_ext = network->GetNeighbohroodExtension();

				// Compute the delta weight of the winner neuron and it of his neighborhood
				for (uint r = row - ne_ext; r < row + ne_ext; r++)
				{
					for (uint c = column - ne_ext; c < column + ne_ext; c++)
					{
						if (!network->Ucc(r, c))
							continue;

						for (uint i = 0; i < network->GetOutputNeuronAt(r, c)->GetInSyn()->size(); i++)
						{
							network->GetOutputNeuronAt(r, c)->GetSynapseAt(i)->SetUpdateWeight(
								learningRate *
								(network->GetOutputNeuronAt(r, c)->GetSynapseAt(i)->GetPreNeuron()->Output() -
								network->GetOutputNeuronAt(r, c)->GetSynapseAt(i)->GetWeight())
								);
							network->GetOutputNeuronAt(r, c)->GetSynapseAt(i)->Update();

							// Get maximum update
							if (fabs(network->GetOutputNeuronAt(r, c)->GetSynapseAt(i)->GetUpdateWeight()) > update)
								update = fabs(network->GetOutputNeuronAt(r, c)->GetSynapseAt(i)->GetUpdateWeight());
						}
					}
				}
			}

			updateHistory.push_back(update);

			// Check the update
			if (updateHistory[updateHistory.size() - 1] == 0)
				break;
			// If maximum update value of the all pattern set is zero, the learning has been terminated

			// Decay of learning rate and neighborhood extension
			learningRate -= lrateDecayFact;
			local_NeighbohroodExtension -= neighDecayFact;
			network->SetNeighbohroodExtension((uint)local_NeighbohroodExtension);
		}
		// Restore Neighbohrood extension
		network->RestoreNeighbohroodExtension();
		// Restore learning rate
		learningRate = orig_lrate;
	}
}