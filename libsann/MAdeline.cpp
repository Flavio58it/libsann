#include "MAdeline.h"

namespace libsann
{
  MAdeline::MAdeline(InputLayer* input, FeedForwardLayer* output)
  {
    inLayer = input;
    outLayer = output;
    epochsForTraining = 0;

    if ((inLayer == NULL) || (outLayer == NULL))
      throw(new exception("Layers configuration not valid. Feed-forward networks must have at least one input, one hidden and one output layer."));
  }

  MAdeline::~MAdeline()
  {
    delete inLayer;
    delete outLayer;
  }

  void MAdeline::BuildNet(bool addBias, SynInitMode mode)
  {
    // reset current connestions
    outLayer->RemoveAllConnections();

    if (addBias)
    {
      inLayer->CreateBias();
    }

    for (uint i = 0; i < outLayer->GetNeurons()->size(); i++)
    {
      for (uint j = 0; j < inLayer->GetNeurons()->size(); j++)
      {
        Synapse* s = new Synapse();
        s->SetWeight(0);
        s->ConnectPre(inLayer->GetNeuronAt(j));
        s->ConnectPost(outLayer->GetNeuronAt(i));

        outLayer->GetNeuronAt(i)->AddSynapse(s);
        inLayer->GetNeuronAt(j)->AddSynapseOut(s);
      }
    }

    Reset(mode);
  }

  void MAdeline::Reset(SynInitMode mode)
  {
    switch (mode)
    {
    case SynInitMode::NGUYEN_WINDROW:
    {
                                                 throw(new exception("Nguyen-Windrow initialization not supported for M-Adeline."));
    }
    default:
      break;
    }

    outLayer->Reset(mode);
  }

  void MAdeline::Exec()
  {
    if ((inLayer == NULL) || (outLayer == NULL))
      throw(new exception(
      "Layers configuration not valid. Multi-layer perceptron must have at least one input, one hidden and one output layer."
      ));

    for (uint i = 0; i < outLayer->GetNeurons()->size(); i++)
    {
      outLayer->GetNeuronAt(i)->AcquiringPotential();
    }
  }
}
