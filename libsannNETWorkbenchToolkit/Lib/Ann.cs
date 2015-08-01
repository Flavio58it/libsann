using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using libsannNET;
using log4net;

namespace libsannNETWorkbenchToolkit.Lib
{
    using libsannNETWorkbenchToolkit.Configuration;
    using libsannNETWorkbenchToolkit.Utils;
    using libsannNETWorkbenchToolkit.ExceptionHandling;

    /// <summary>
    /// Network status
    /// </summary>
    public enum NetworkStatus
    {
        TRAINING,
        TRAINED,
        UNTRAINED,
        VALIDATION,
        VALIDATED,
        ERROR
    };

    /// <summary>
    /// Class artificial neural network
    /// </summary>
    public class Ann : ObjectLoggable
    {
        protected NetworkStatus status;
        protected Mlp network;
        protected SynInitMode synInitMode;
        protected double errorTarget;

        public Ann()
        { }

        public NetworkStatus Status
        {
            get
            {
                return status;
            }
        }

        public void Build(AnnBuild AnnBuilding)
        {
            if(network != null)
                network.Destroy();

            network = new Mlp(
                AnnBuilding.InputUnits,
                AnnBuilding.HiddenUnits,
                AnnBuilding.HiddenLayers,
                AnnBuilding.OutputUnits,
                AnnBuilding.Bias,
                AnnBuilding.ActivationNeuroMode
                );
            
            synInitMode = AnnBuilding.SynInitMode;

            network.Instantiate();

            status = NetworkStatus.UNTRAINED;
            
            logger.InfoFormat("Building neural network classifier: {0}", this.ToString());
        }

        public void CreateBackPropagationTrainingAlgorithm(AnnBuild annComp, BackPropagation prop)
        {
            errorTarget = annComp.ErrorTarget;
            network.InstantiateBackPropagationAlgorithm(
                errorTarget, annComp.MaxEpochs, prop.LearningRate, prop.Beta);

            network.BuildStatsListener();
        }

        public void CreateResilientPropagationAlgorithm(AnnBuild annComp, ResilientPropagation rprop)
        {
            errorTarget = annComp.ErrorTarget;
            network.InstantiateResilientPropagationAlgorithm(
                errorTarget, annComp.MaxEpochs, rprop.MaxUpdateValue, rprop.MinUpdateValue, rprop.GrowthFactor, rprop.DecreaseFactor);

            network.BuildStatsListener();
        }

        public async Task<bool> TrainingAsync(double[][] inPatterns, double[][] outPatterns)
        {
            status = NetworkStatus.UNTRAINED;
            bool res = false;

            try
            {
                if(inPatterns[0].Count() != network.InputSize)
                    throw new Exception("Input size wrong");

                res = await network.TrainingAsync(inPatterns, outPatterns, errorTarget, synInitMode);
                
                if(res)
                    status = NetworkStatus.TRAINED;

            } catch(Exception e)
            {
                status = NetworkStatus.ERROR;
                throw e;
            }

            return res;
        }

        public void AbortTraining()
        {
            status = NetworkStatus.UNTRAINED;
            network.AbortTraining();
        }

        public bool Validate(double[][] inValPatterns, double[][] outValPatterns)
        {
            status = NetworkStatus.VALIDATION;
            bool res = false;

            try
            {
                double eErr = 0;
                double pErr = 0;

                for(int i = 0; i < inValPatterns.Count(); i++)
                {
                    double[] output = network.Exec(inValPatterns[i]);
                    pErr = 0;
                    for(int j = 0; j < output.Count(); j++)
                    {
                        pErr += Math.Pow(outValPatterns[i][j] - output[j], 2);
                    }
                    pErr *= (1.0 / Convert.ToDouble(outValPatterns[i].Count()));
                    eErr += pErr;
                }
                eErr *= (1.0 / Convert.ToDouble(inValPatterns.Count()));
                res = (eErr <= errorTarget);

                if(res)
                    status = NetworkStatus.VALIDATED;
            } catch(Exception e)
            {
                status = NetworkStatus.ERROR;
                logger.ErrorFormat(ExceptionManager.Parse(e) + Environment.NewLine + e.StackTrace);
            }

            return res;
        }

        public override string ToString()
        {
            return string.Format("Inputs: {0}, Hidden layers: {1}, Hidden units: {2}, Outputs: {3}, Activation mode: {4}",
                network.InputSize, network.HiddenLayers, network.HiddenUnits.ToArray(), network.OutputSize, network.TransferFunction.ToString());
        }

        public double[] ErrorsLastTraining()
        {
            return network.GetErrorValues();
        }

        public List<double[][]> OutputValuesLastTraining()
        {
            return network.GetOutputValues();
        }

        public double[] WeightsLastTraining()
        {
            return network.GetSynapsesWeights();
        }

        public double CurrentMse()
        {
            if (network != null)
                return network.CurrentError;
            else 
                return double.MaxValue;
        }
    }
}