# libsann - Neural networks library 

## What's inside

**libsann**

libsann is a little [artificial neural network](https://en.wikipedia.org/wiki/Artificial_neural_network) framework written in C++.

Provide the following configurations:

- M-Adeline
- Feed Forward
- Kohonen Map
- Elman Feed Forward

And the following learning algorithms:

- Windrow Hoff
- Auto organizing
- Back propagation
- Resilient propagation

**libsannNET**

Basic wrapper to use libsann in .Net

> The Wrapper is only for the feed forward configuration (the most usesd in the applications)

**libsannNETWorkbenchToolkit**

Workbench application to test patterns set and performance of the network configurations.
You can choose a configuration, load patterns, set the learning method and see the network results and learning time in a graph.

## How to use

Usinge the library is very simple with any configuration, for example, to solve the parity problem with a multi perceptron network trained by resilient propagation:

```C#
double[][] parityInput =
    {
        new [] {0.0,0.0,0.0},
        new [] {0.0,0.0,1.0},
        new [] {0.0,1.0,0.0},
        new [] {0.0,1.0,1.0},
        new [] {1.0,0.0,0.0},
        new [] {1.0,0.0,1.0},
        new [] {1.0,1.0,0.0},
        new [] {1.0,1.0,1.0}
    };
double[][] parityOutput =
    {
        new [] {1.0},
        new [] {0.0},
        new [] {0.0},
        new [] {1.0},
        new [] {0.0},
        new [] {1.0},
        new [] {1.0},
        new [] {0.0}
    };
double[][] parityInputValidation =
    {
        new [] {0.01,0.0,0.05},
        new [] {0.0,0.08,0.9},
        new [] {0.08,1.0,0.07},
        new [] {0.054,0.95,0.95},
        new [] {1.0,0.0,0.06},
        new [] {0.9,0.0,0.94},
        new [] {0.99,1.0,0.09},
        new [] {0.92,0.9,0.95}
    };

var hiddenLayers = new uint[1]
{
    4
};

Mlp mlpNetwork = new Mlp(3, 1, hiddenLayers, 1);
                 
double targetError = 0.05;
uint maxEpochs = 10000;

mlpNetwork.InstantiateResilientPropagationAlgorithm(targetError, maxEpochs);
Task<bool> trainResult = mlpNetwork.TrainingAsync(parityInput, parityOutput, targetError);

while(!trainResult.IsCompleted)
    Thread.Sleep(2);

if(!trainResult.Result) 
{
    Console.WriteLine("-> Network training fail");  
    Environment.Exit(-1);
}                  

Console.WriteLine("-> Network trained! [Error: {0}, Epochs: {1}]", mlpNetwork.CurrentError, mlpNetwork.EpochsTraining);

Console.WriteLine("-> Validation:");

int i = 0;
foreach(double[] pattern in parityInputValidation)
{
    double[] output = mlpNetwork.Exec(pattern);
    Console.WriteLine("-> Input: " + pattern[0] + " " + pattern[1]);
    Console.WriteLine("-> Output: " + output[0] + " (ideal: " + parityOutput[i++][0] + ")");
}

Console.WriteLine("-> Validation end");
```

Application output:


>07-Sep-16
>-------------------------
>-> Test network performance for parity problem with rprop

>-> Network trained! [Error: 0.0428488402314767, Epochs: 65]
>
>-> Validation:
>
>-> Input: 0.01 0
>
>-> Output: 0.824452481509327 (ideal: 1)
>
>-> Input: 0 0.08
>
>-> Output: 0.308577961547668 (ideal: 0)
>
>-> Input: 0.08 1
>
>-> Output: 0.0896282266589496 (ideal: 0)
>
>-> Input: 0.054 0.95
>
>-> Output: 0.786683194270234 (ideal: 1)
>
>-> Input: 1 0
>
>-> Output: 0.217482480143022 (ideal: 0)
>
>-> Input: 0.9 0
>
>-> Output: 0.899196794592047 (ideal: 1)
>
>-> Input: 0.99 1
>
>-> Output: 0.621815534099479 (ideal: 1)
>
>-> Input: 0.92 0.9
>
>-> Output: 0.0957467092930296 (ideal: 0)
>
>-> Validation end