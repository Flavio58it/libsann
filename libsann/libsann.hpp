#include "ElmanMlp.h"
#include "MAdeline.h"
#include "Trainingset.h"
#include "BackPropagation.h"
#include "ResilientPropagation.h"
#include "WindrowHoff.h"

/*
*	Pinvoke.hpp contains a collection of general calls for use the library.
*	These functions are exported into a DLL module.
*/

using namespace libsann;

namespace libsannInvoke
{

#pragma region [ Functions _export ]

	extern "C" __declspec(dllexport) void NetSetInput(NetBase*, double*, int);
	extern "C" __declspec(dllexport) double* NetExec(NetBase*, int);
	extern "C" __declspec(dllexport) void libsannInit();
	extern "C" __declspec(dllexport) uint GetEpoch(NetBase*);

	// MLP networks
	extern "C" __declspec(dllexport) Mlp* MlpInstantiate(uint, uint, uint*, uint, bool, ActivationMode);
	extern "C" __declspec(dllexport) void MlpDelete(Mlp*);
	extern "C" __declspec(dllexport) double* MlpGetWeights(Mlp*);
	extern "C" __declspec(dllexport) uint MlpGetWeightsNumber(Mlp*);
	extern "C" __declspec(dllexport) void MlpLoadWeights(Mlp*, double*, int);

	// Elman networks
	extern "C" __declspec(dllexport) ElmanMlp* EMlpInstantiate(uint, uint, uint*, uint, bool, ActivationMode);
	extern "C" __declspec(dllexport) void EMlpDelete(ElmanMlp*);

	// Training Mlp based networks
	extern "C" __declspec(dllexport) double MlpTrainingWithResilientPropagation(Mlp*, double**, double**, uint, uint, uint, double, uint, double, double, double, double, syn_initalization_mode);
	extern "C" __declspec(dllexport) double MlpTrainingWithBackPropagation(Mlp*, double**, double**, uint, uint, uint, double, uint, double, double, syn_initalization_mode);

	// M-adeline networks
	extern "C" __declspec(dllexport) MAdeline* mAdelineInstantiate(uint, uint, bool, ActivationMode);
	extern "C" __declspec(dllexport) double mAdelineTrainingWithWindrowHoff(MAdeline*, double**, double**, uint, uint, uint, double, uint, double, syn_initalization_mode);
	extern "C" __declspec(dllexport) void mAdelineDelete(MAdeline*);

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
		net->BuildNet(true, syn_initalization_mode::NGUYEN_WINDROW);

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

	double MlpTraining(Mlp* net, Trainingset *set, PropagationBase *trainAlg, syn_initalization_mode mode)
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
	// Training the network with resilient back-propagation, return the final error 
	///////////////////////////////////////////////////////////////////////////////////////
	__declspec(dllexport) double MlpTrainingWithResilientPropagation(
		Mlp* net,							// Pointer to a Mlp network
		double **inputPatterns,				// The input patterns matrix
		double **outputPatterns,			// The output patterns matrix
		uint inputRows,						// Number of pattern of the set
		uint num_input_units,				// Number of the input neurons of the network
		uint num_units_for_output_layer,	// Number of the output neurons of the network
		double errorTarget,					// Target error
		uint maxEpochs,						// Maximum training epochs
		double maxUpdateValue,				// Maximum update value
		double minupdateValue,				// Minimum update value
		double growthFactor,				// Growth factor (local learning rate)
		double decreaseFactor,				// Decrease factor (local learning rate)
		syn_initalization_mode mode			// Weights initialization mode
		)
	{
		// Build the training algorithm
		ResilientPropagation *trainAlg = new ResilientPropagation(
			net, errorTarget, maxEpochs, maxUpdateValue, minupdateValue, growthFactor, decreaseFactor);

		// Create the training set
		Trainingset *set = new Trainingset(
			inputPatterns, outputPatterns, inputRows, num_input_units, num_units_for_output_layer);

		return MlpTraining(net, set, trainAlg, mode);
	}

	///////////////////////////////////////////////////////////////////////////////////////
	// Training the network with back-propagation (adaptive learning rate), return the final error 
	///////////////////////////////////////////////////////////////////////////////////////
	__declspec(dllexport) double MlpTrainingWithBackPropagation(
		Mlp* net,							// Pointer to a Mlp network
		double **inputPatterns,				// The input patterns matrix
		double **outputPatterns,			// The output patterns matrix
		uint inputRows,						// Number of pattern of the set
		uint num_input_units,				// Number of the input neurons of the network
		uint num_units_for_output_layer,	// Number of the output neurons of the network
		double errorTarget,					// Target error
		uint maxEpochs,						// Maximum training epochs
		double learnginRate,				// Learning rate
		double beta,						// Beta rate for momentum
		syn_initalization_mode mode			// Weights initialization mode
		)
	{
		// Build the training algorithm
		BackPropagation *trainAlg = new BackPropagation(
			net, errorTarget, maxEpochs, learnginRate, beta);

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
		Mlp* net,			// Pointer to a Mlp network
		double* weights,	// Weights
		int num				// Number of weights
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
		double** inputPatterns,			// The input patterns matrix
		double** outputPatterns,		// The output patterns matrix
		uint inputRows,					// Number of pattern of the set
		uint num_input_units,			// Number of the input neurons of the network
		uint num_units_of_output_layer,	// Number of the output neurons of the network
		double errorTarget,				// Target error
		uint maxEpochs,					// Maximum training epochs
		double learningRate,			// Learning rate
		syn_initalization_mode mode		// Weights initialization mode
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