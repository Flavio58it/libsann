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
*	Pinvoke.hpp contains a collection of general calls for use the library.
*	These functions are exported into a DLL module.
*/

#include "ElmanMlp.h"
#include "MAdeline.h"
#include "Trainingset.h"
#include "BackPropagation.h"
#include "ResilientPropagation.h"
#include "WindrowHoff.h"

using namespace libsann;

namespace libsannInvoke
{

#pragma region [ Functions _export ]

#ifdef __cplusplus
  extern "C"
  {
#endif
    __declspec(dllexport) void NetSetInput(NetBase*, double*, int);
    __declspec(dllexport) double* NetExec(NetBase*, int);
    __declspec(dllexport) void libsannInit();
    __declspec(dllexport) uint GetEpoch(NetBase*);

    // MLP networks
    __declspec(dllexport) Mlp* MlpInstantiate(uint, uint, uint*, uint, bool, ActivationMode);
    __declspec(dllexport) void MlpDelete(Mlp*);
    __declspec(dllexport) double* MlpGetWeights(Mlp*);
    __declspec(dllexport) uint MlpGetWeightsNumber(Mlp*);
    __declspec(dllexport) void MlpLoadWeights(Mlp*, double*, int);

    // Elman networks
    __declspec(dllexport) ElmanMlp* EMlpInstantiate(uint, uint, uint*, uint, bool, ActivationMode);
    __declspec(dllexport) void EMlpDelete(ElmanMlp*);

    // Training Mlp based networks
    __declspec(dllexport) BackPropagation* InstantiateBackPropagationTraininglgorithm(Mlp*, double, uint, double, double);
    __declspec(dllexport) ResilientPropagation* InstantiateResilientPropagationTrainingAlgorithm(Mlp*, double, uint, double, double, double, double);
    __declspec(dllexport) double MlpTrainingWithPropagation(Mlp*, double**, double**, uint, uint, uint, PropagationBase*, SynInitMode);

    // Propagation training statistical utility structure
    __declspec(dllexport) PropStatsListener* BuildStatsListener(PropagationBase*);
    __declspec(dllexport) void DestroyPropStatsListener(PropagationBase*);
    __declspec(dllexport) double*** GetPropOutputValues(PropStatsListener*, int*);
    __declspec(dllexport) double* GetPropErrorValue(PropStatsListener*, int*);
    __declspec(dllexport) void ResetPropStats(PropStatsListener*);

    // M-adeline networks
    __declspec(dllexport) MAdeline* mAdelineInstantiate(uint, uint, bool, ActivationMode);
    __declspec(dllexport) double mAdelineTrainingWithWindrowHoff(MAdeline*, double**, double**, uint, uint, uint, double, uint, double, SynInitMode);
    __declspec(dllexport) void mAdelineDelete(MAdeline*);
#ifdef __cplusplus
  }
#endif

#pragma endregion

#pragma region [ Others functions ]

  ///////////////////////////////////////////////////////////////////////////////////////
  // Initialize library
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void libsannInit()
  {
    INIT;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Set network input
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void NetSetInput(
    NetBase* net,	// Pointer to a network
    double *data,	// Pointer to the pattern
    int size		// Size of the pattern
    )
  {
    vector<double> _in;
    for (int i = 0; i < size; i++)
      _in.push_back(data[i]);

    net->SetInput(_in);
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Return the current epoch
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) uint GetEpoch(
    NetBase* net	// Pointer to a network
    )
  {
    return net->GetEpochsForTraining();
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Execute and return the output values
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) double* NetExec(
    NetBase* net,	// Pointer to a NetBase network
    int outputSize	// Number of output neurons
    )
  {
    net->Exec();
    double *_output = new double[outputSize];

    for (int i = 0; i < outputSize; i++)
    {
      _output[i] = net->GetOutputNeurons()->at(i)->Output();
    }

    return _output;
  }

#pragma endregion

#pragma region [ Library functions ]

#pragma region [ Mlp ]

#pragma region [ Elman Mlp ]

  ///////////////////////////////////////////////////////////////////////////////////////
  // Build a new Elman mlp network and returns it as pointer
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) ElmanMlp* EMlpInstantiate(
    uint num_input_units,				// Number of the input neurons of the network
    uint num_hidden_layers,				// Number of the hidden layers
    uint *num_units_for_hidden_layers,	// Number of the neurons for each hidden layers
    uint num_units_for_output_layer,	// Number of the output neurons of the network
    bool useBias,						// Use bias flag
    ActivationMode mode					// Transfer function of the hidden and output neurons
    )
  {
    // Create array of hidden layers
    vector<FeedForwardLayer*> LayersHidden;
    for (uint i = 0; i < num_hidden_layers; i++)
      LayersHidden.push_back(new FeedForwardLayer(num_units_for_hidden_layers[i], mode));

    // Create the 'Multi-layered feedforward network' structure
    ElmanMlp *net = new ElmanMlp(
      new InputLayer(num_input_units),
      LayersHidden,
      new FeedForwardLayer(num_units_for_output_layer, mode)
      );

    // Connects all
    net->BuildNet(true, SynInitMode::NGUYEN_WINDROW);

    return net;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Delete network object
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void EMlpDelete(
    ElmanMlp* net	// Pointer to a Mlp network
    )
  {
    delete net;
  }

#pragma endregion

  double MlpTraining(Mlp* net, Trainingset *set, PropagationBase *trainAlg, SynInitMode mode)
  {
    // Set training set
    trainAlg->SetTrainingSet(set);

    // Training
    trainAlg->Training(mode, false);
    double error = trainAlg->GetCurrentError();

    delete set;
    delete trainAlg;

    return error;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Build a new Multi-layered feedforward network model, and return it as pointer 
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) Mlp* MlpInstantiate(
    uint num_input_units,				// Number of the input neurons of the network
    uint num_hidden_layers,				// Number of the hidden layers
    uint *num_units_for_hidden_layers,	// Number of the neurons for each hidden layers
    uint num_units_for_output_layer,	// Number of the output neurons of the network
    bool useBias,						// Use bias flag
    ActivationMode mode					// Transfer function of the hidden and output neurons
    )
  {
    // Create array of hidden layers
    vector<FeedForwardLayer*> LayersHidden;
    for (uint i = 0; i < num_hidden_layers; i++)
      LayersHidden.push_back(new FeedForwardLayer(num_units_for_hidden_layers[i], mode));

    // Create the 'Multi-layered feedforward network' structure
    Mlp *net = new Mlp(
      new InputLayer(num_input_units),
      LayersHidden,
      new FeedForwardLayer(num_units_for_output_layer)
      );

    // Connects all
    net->BuildNet(true);

    return net;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Instantiate Resilient Propagation algorithm
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) ResilientPropagation* InstantiateResilientPropagationTrainingAlgorithm(
    Mlp* net,					// Pointer to a Mlp network
    double errorTarget,			// Target error
    uint maxEpochs,				// Maximum training epochs
    double maxUpdateValue,		// Maximum update value
    double minupdateValue,		// Minimum update value
    double growthFactor,		// Growth factor (local learning rate)
    double decreaseFactor		// Decrease factor (local learning rate)
    )
  {
    // Build the training algorithm
    ResilientPropagation *trainAlg = new ResilientPropagation(
      net, errorTarget, maxEpochs, maxUpdateValue, minupdateValue, growthFactor, decreaseFactor);

    return trainAlg;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Instantiate Back Propagation algorithm
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) BackPropagation* InstantiateBackPropagationTraininglgorithm(
    Mlp* net,						// Pointer to a Mlp network
    double errorTarget,				// Target error
    uint maxEpochs,					// Maximum training epochs
    double learnginRate,			// Learning rate
    double beta						// Beta rate for momentum
    )
  {
    // Build the training algorithm
    BackPropagation *trainAlg = new BackPropagation(
      net, errorTarget, maxEpochs, learnginRate, beta);

    return trainAlg;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Training the network with resilient back-propagation, return the final error 
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) double MlpTrainingWithPropagation(
    Mlp* net,						// Pointer to a Mlp network
    double **inputPatterns,			// The input patterns matrix
    double **outputPatterns,		// The output patterns matrix
    uint inputRows,					// Number of pattern of the set
    uint num_input_units,			// Number of the input neurons of the network
    uint num_units_for_output_layer,// Number of the output neurons of the network
    PropagationBase* trainAlg,      // Training algorithm
    SynInitMode mode			    // Weights initialization mode
    )
  {
    // Create the training set
    Trainingset *set = new Trainingset(
      inputPatterns, outputPatterns, inputRows, num_input_units, num_units_for_output_layer);

    return MlpTraining(net, set, trainAlg, mode);
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Delete network object
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void MlpDelete(
    Mlp* net	// Pointer to a Mlp network
    )
  {
    delete net;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Get weights
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) double* MlpGetWeights(
    Mlp* net	// Pointer to a Mlp network
    )
  {
    return net->SaveSynapsesToArray();
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Set weights
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void MlpLoadWeights(
    Mlp* net,			    // Pointer to a Mlp network
    double* weights,		// Weights
    int num				    // Number of weights
    )
  {
    vector<double> v;
    for (int i = 0; i < num; i++)
      v.push_back(weights[i]);
    net->LoadSynapses(v);
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Get synapses amount
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) uint MlpGetWeightsNumber(
    Mlp* net	// Pointer to a Mlp network
    )
  {
    return net->SynapsesNumber();
  }

#pragma endregion

#pragma region Statistics utility

  ///////////////////////////////////////////////////////////////////////////////////////
  // Destroy the PropStatsListener object
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void DestroyPropStatsListener(PropagationBase* trainingPropAlg)
  {
    trainingPropAlg->DestroyStatsListener();
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Build a PropStatsListener object
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) PropStatsListener* BuildStatsListener(PropagationBase* trainingPropAlg)
  {
    return trainingPropAlg->BuildStatsListener();
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Get output values in real time during training
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) double*** GetPropOutputValues(
    PropStatsListener* listener,  // Pointer to listener propagation structure
    int *_out_patterns_count
    )
  {
    int patternSize;
    vector<double**> out = listener->GetOutputValues();

    double*** sMatrix = (double***)malloc(sizeof(double**)*out.size());
    for (int i = 0; i < out.size(); i++)
    {
      sMatrix[i] = out[i];
    }
    
    *_out_patterns_count = out.size();
    return sMatrix;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Get error value in real time during training
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) double* GetPropErrorValue(
    PropStatsListener* listener, // Pointer to listener propagation structure
    int *_out_count
    )
  {
    vector<double> errors = listener->GetErrorValue();
    if (errors.size() > 0)
    {
      double* retArray = new double[errors.size()];
      memcpy(retArray, &errors[0], sizeof(double)*errors.size());
      *_out_count = errors.size();
      return retArray;
    }
    else
      return new double[0];
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Reset statistics
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void ResetPropStats(
    PropStatsListener* listener
    )
  {
    listener->Reset();
  }

#pragma endregion

#pragma region [ M-Adeline ]

  ///////////////////////////////////////////////////////////////////////////////////////
  // Build a new MAdeline network and returns it as pointer
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) MAdeline* mAdelineInstantiate(
    uint num_input_units,	// Number of input neurons
    uint num_output_units,	// Number of output neurons
    bool useBias,			// Use bias flag
    ActivationMode mode		// Activation mode
    )
  {
    InputLayer *inLayer = new InputLayer(num_input_units);
    FeedForwardLayer *outLayer = new FeedForwardLayer(num_output_units, mode);

    MAdeline *net = new MAdeline(inLayer, outLayer);

    net->BuildNet(useBias);

    return net;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Training MAdeline network and return the final error
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) double mAdelineTrainingWithWindrowHoff(
    MAdeline *net,					// Pointer to a M-Adeline network
    double** inputPatterns,		    // The input patterns matrix
    double** outputPatterns,		// The output patterns matrix
    uint inputRows,					// Number of pattern of the set
    uint num_input_units,			// Number of the input neurons of the network
    uint num_units_of_output_layer,	// Number of the output neurons of the network
    double errorTarget,				// Target error
    uint maxEpochs,					// Maximum training epochs
    double learningRate,			// Learning rate
    SynInitMode mode		        // Weights initialization mode
    )
  {
    // Create the training set
    Trainingset *set = new Trainingset(
      inputPatterns, outputPatterns, inputRows, num_input_units, num_units_of_output_layer);

    // Create the training algorithm
    WindrowHoff *trainAlg = new WindrowHoff(net, errorTarget, maxEpochs, learningRate);

    // Set training set
    trainAlg->SetTrainingSet(set);

    // Training
    trainAlg->training(mode);
    double error = trainAlg->GetCurrentError();

    delete set;
    delete trainAlg;

    return error;
  }

  ///////////////////////////////////////////////////////////////////////////////////////
  // Delete network object
  ///////////////////////////////////////////////////////////////////////////////////////
  __declspec(dllexport) void mAdelineDelete(
    MAdeline* net	// Pointer to a M-Adeline network
    )
  {
    delete net;
  }

#pragma endregion

#pragma region [ Self-organizing ]

  /*
  * Not implemented.
  * Given the need to access much contact with the Kohonen map, is disadvantageous to create an interface to be used in other languages.
  * Use a version directly implemented in the desired language.
  */

#pragma endregion

#pragma endregion
};