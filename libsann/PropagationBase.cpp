#include "PropagationBase.h"

namespace libsann
{
	void PropagationBase::Training(SynInitMode mode, bool OnLine, bool Mt)
	{
#ifdef MATLAB_DEBUG
		matlab_interface *m = new matlab_interface();
#endif
		// Initialization

		// Training local variables
		double pErr;		  // Error over the single pattern
		double eErr = 0;	// Error over the entire epoch
		epoch = 0;			  // Epochs counter	
		// Reset network memory by synapses randomization
		network->Reset(mode);
		network->SetEpochsForTraining(epoch);

    vector<double*> outputs;

		for (;;)
		{
			for (uint i = 0; i < set->size(); i++)
			{
				// Set 'i'th input pattern
				network->SetInput(set->GetPatternAt(i)->GetDataInput());

#ifdef USE_BOOST
				// Execution
				if (Mt)
					network->Mt_exec();
				else
					network->Exec();
#else
				network->Exec();
#endif
        outputs.push_back(network->GetOutputValues());

				// Update error on pattern 'i' (mean square error on pattern)
				pErr = 0;
				for (uint j = 0; j < network->GetOutputNeurons()->size(); j++)
				{
					pErr += pow(set->GetPatternAt(i)->GetDataOutput()[j] - network->GetOutputNeuronAt(j)->Output(), 2);
				}
				pErr *= (1.0 / (double)network->GetOutputNeurons()->size());

				// Update error over the entire epoch
				eErr += pErr;

				/* Compute derivatives for all connections of the network */
         
				// Compute delta for the neurons of output layer
				for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
				{
					network->GetOutputNeuronAt(o)->SetDelta(
						network->GetOutputNeuronAt(o)->Derivative() * (set->GetPatternAt(i)->GetDataOutput()[o] - network->GetOutputNeuronAt(o)->Output())
						);
					// Compute derivatives for each synapse of the layer
					for (uint s = 0; s < network->GetOutLayer()->GetNeuronAt(o)->GetInSyn()->size(); s++)
					{
						network->GetOutLayer()->GetNeuronAt(o)->GetSynapseAt(s)->ComputeDerivative(network->GetOutLayer()->GetNeuronAt(o)->GetDelta());
					}
				}

				for (int h = network->GetHiddenLayers()->size() - 1; h >= 0; h--)
				{
					// Back prop the error
					for (uint n = 0; n < network->GetHiddenLayerAt(h)->GetNeurons()->size(); n++)
					{
						if (!network->GetHiddenLayerAt(h)->GetNeuronAt(n)->IsBiasNeuron())
						{
							network->GetHiddenLayerAt(h)->GetNeuronAt(n)->AcquiringDeltaError();
						}
					}

					// Compute delta for hidden layers and their neurons
					for (uint n = 0; n < network->GetHiddenLayerAt(h)->GetNeurons()->size(); n++)
					{
						if (!network->GetHiddenLayerAt(h)->GetNeuronAt(n)->IsBiasNeuron())
						{
							network->GetHiddenLayerAt(h)->GetNeuronAt(n)->SetDelta(
								network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetError() * network->GetHiddenLayerAt(h)->GetNeuronAt(n)->Derivative()
								);

							// Compute derivatives for each synapse of the layer
							for (uint s = 0; s < network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
							{
								network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->ComputeDerivative(network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetDelta());
							}
						}
					}
				}
				if (OnLine)
				{
					OnLineLearning();
					Adjust();
				}
				else
					BatchLearning();
			}
			// Compute and checks the error of the network (mse)		
			eErr *= (1.0 / (double)set->size());	// Mean square error over the entire trainig set

			currentError = eErr;

			// Add network error to debug vector
			NetErrors.push_back(currentError);

      // Notifications
      __raise CurrentErrorValue(currentError);
      __raise CurrentValues(listener->PrepareValuesForStats(outputs,set), set->size());

      outputs.clear();

			if (currentError <= errorTarget)
			{
				network->SetEpochsForTraining(epoch);
				break;	// Training end, target error has been reached
			}

			// Check number of epochs
			if (maxEpochs <= ++epoch)
				break;

			// Batch
			if (!OnLine)
			{
				// Update the weights
				Adjust();
				// Clean after update
				CleanUpdateWeights();
			}

			// else continue training
			eErr = 0;
		}
#ifdef MATLAB_DEBUG
		m->matlab_plotArray(&NetErrors[0], (int)NetErrors.size(), "mse", NULL, NULL);
#endif
	}

	void PropagationBase::SetTrainingSet(Trainingset* _set)
	{
		set = _set;
	}

	void PropagationBase::Adjust()
	{
		// Adjust all weights after learning process
		for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
		{
			if (!network->GetOutputNeuronAt(o)->IsBiasNeuron())
			{
				for (uint s = 0; s < network->GetOutputNeuronAt(o)->GetInSyn()->size(); s++)
				{
					network->GetOutputNeuronAt(o)->GetSynapseAt(s)->AddMomentum(beta);
					network->GetOutputNeuronAt(o)->GetSynapseAt(s)->Update();
				}
			}
		}
		for (uint h = 0; h < network->GetHiddenLayers()->size(); h++)
		{
			for (uint n = 0; n < network->GetHiddenLayerAt(h)->GetNeurons()->size(); n++)
			{
				if (!network->GetHiddenLayerAt(h)->GetNeuronAt(n)->IsBiasNeuron())
				{
					for (uint s = 0; s < network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
					{
						network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->AddMomentum(beta);
						network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->Update();
					}
				}
			}
		}
	}

	void PropagationBase::CleanUpdateWeights()
	{
		for (uint o = 0; o < network->GetOutputNeurons()->size(); o++)
		{
			if (!network->GetOutputNeuronAt(o)->IsBiasNeuron())
			{
				for (uint s = 0; s < network->GetOutputNeuronAt(o)->GetInSyn()->size(); s++)
				{
					network->GetOutputNeuronAt(o)->GetSynapseAt(s)->CleanGradients();
				}
			}
		}
		for (uint h = 0; h < network->GetHiddenLayers()->size(); h++)
		{
			for (uint n = 0; n < network->GetHiddenLayerAt(h)->GetNeurons()->size(); n++)
			{
				if (!network->GetHiddenLayerAt(h)->GetNeuronAt(n)->IsBiasNeuron())
				{
					for (uint s = 0; s < network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetInSyn()->size(); s++)
					{
						network->GetHiddenLayerAt(h)->GetNeuronAt(n)->GetSynapseAt(s)->CleanGradients();
					}
				}
			}
		}
	}

  PropStatsListener* PropagationBase::BuildStatsListener()
  {
    listener = new PropStatsListener();
    __hook(&PropagationBase::CurrentValues, this, &PropStatsListener::OutputValuesHandler, listener);
    __hook(&PropagationBase::CurrentErrorValue, this, &PropStatsListener::ErrorValueHandler, listener);
    return listener;
  }

  void PropagationBase::DestroyStatsListener()
  {
    __unhook(&PropagationBase::CurrentValues, this, &PropStatsListener::OutputValuesHandler, listener);
    __unhook(&PropagationBase::CurrentErrorValue, this, &PropStatsListener::ErrorValueHandler, listener);
  }
}