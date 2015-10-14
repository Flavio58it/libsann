using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Ninject;
using log4net;

namespace libsannNETWorkbenchToolkit
{
    using libsannNETWorkbenchToolkit.Lib;
    using libsannNETWorkbenchToolkit.Set;
    using libsannNETWorkbenchToolkit.Utils;
    using libsannNETWorkbenchToolkit.Configuration;
    using libsannNETWorkbenchToolkit.ExceptionHandling;

    public enum TrainingResults : byte
    {
        MODEL_INVALID = 0,
        CONVERGENCE_NOT_REACHED = 1,
        VALIDATION_FAIL = 2,
        NOT_VALIDATED = 3,
        TRAINING_SUCCESS = 4,
        INTERNAL_ERROR = 5
    }

    /// <summary>
    /// Application core engine
    /// </summary>
    public class AppCore : ObjectLoggable
    {
        protected Ann ann;
        protected AnnBuild annConfig;
        protected bool pollingStop = false;
        protected string error;

        protected List<double> mse;
        protected List<double[][]> outputs;
        protected List<double> weights;

        internal event EventHandler<List<double>> MseUpdate;
        internal event EventHandler<List<double[][]>> OutputsUpdate;
        internal event EventHandler<List<double>> WeightsUpdate;

        public List<double> CurrentMse
        {
            get
            {
                return mse;
            }
        }

        public List<double[][]> CurrentOutputs
        {
            get
            {
                return outputs;
            }
        }

        public List<double> CurrentWeights
        {
            get
            {
                return weights;
            }
        }

        public string LastError
        {
            get
            {
                return error;
            }
        }

        public AppCore()
        {
            Setup();
        }

        protected void Setup()
        {
            // Network
            annConfig = NinjectBinding.GetKernel.Get<AnnBuild>();

            ann = new Ann();
            ann.Build(annConfig);

            mse = new List<double>();
            outputs = new List<double[][]>();
            weights = new List<double>();

            logger.InfoFormat("Neural network has been built");
        }

        public async Task<TrainingResults> Train(SetModel model)
        {
            TrainingResults returnCode;

            try
            {
                if (!model.IsValid)
                {
                    returnCode = TrainingResults.MODEL_INVALID;
                    logger.ErrorFormat("The model is not valid");
                }
                else
                {
                    if (annConfig.LearningMode == Global.LearningMode.RESILIENT_PROPAGATION)
                    {
                        ann.CreateResilientPropagationAlgorithm(annConfig, NinjectBinding.GetKernel.Get<ResilientPropagation>());
                        logger.InfoFormat("Created resilient propagation algorithm");
                    }
                    else if (annConfig.LearningMode == Global.LearningMode.BACK_PROPAGATION)
                    {
                        ann.CreateBackPropagationTrainingAlgorithm(annConfig, NinjectBinding.GetKernel.Get<BackPropagation>());
                        logger.InfoFormat("Created back-propagation algorithm");
                    }

                    // Reset stats
                    mse.Clear();
                    outputs.Clear();
                    weights.Clear();

                    bool trainingRes = false;
                    Thread polling = new Thread(NetworkPolling);

                    try
                    {
                        polling.Start();

                        logger.InfoFormat("Wait for network training ... ");

                        trainingRes = await ann.TrainingAsync(model.inMatrix, model.outMatrix);

                        logger.InfoFormat("Network training end. Epoch: {0}, Error: {1}",mse.Count,ann.CurrentMse());
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        pollingStop = true;
                        polling.Join();
                    }

                    if (!trainingRes)
                    {
                        returnCode = TrainingResults.CONVERGENCE_NOT_REACHED;
                        logger.InfoFormat("Training fail");
                    }
                    else
                    {
                        logger.InfoFormat("Training success");

                        if (!model.HasValidData)
                        {
                            returnCode = TrainingResults.NOT_VALIDATED;
                            logger.WarnFormat("Network has no validation data. Skip validation");
                        }
                        else
                        {
                            logger.InfoFormat("Validation ...");

                            bool validationRes = ann.Validate(model.inValMatrix, model.outValMatrix);

                            if (!validationRes)
                            {
                                returnCode = TrainingResults.VALIDATION_FAIL;
                                logger.InfoFormat("Validation fail");
                            }
                            else
                                logger.InfoFormat("Validation success");
                        }

                        returnCode = TrainingResults.TRAINING_SUCCESS;
                    }
                }
            } catch(Exception e)
            {
                logger.ErrorFormat(ExceptionManager.Parse(e) + Environment.NewLine + e.StackTrace);
                error = e.Message;
                returnCode = TrainingResults.INTERNAL_ERROR;
            }

            return returnCode;
        }

        public void AbortTraining()
        {
            ann.AbortTraining();
        }

        public double LastMse()
        {
            return ann.CurrentMse();
        }

        public int TrainingEpoch()
        {
            return mse.Count;
        }

        protected void NetworkPolling()
        {
            logger.InfoFormat("Start network polling for notification");
            for(; ; )
            {
                try
                {
                    if(pollingStop == true)
                    {
                        pollingStop = false;
                        break;
                    }

                    // Collect stats
                    weights.Clear();
                    outputs.Clear();
                    outputs.AddRange(ann.OutputValuesLastTraining());
                    weights.AddRange(ann.WeightsLastTraining());
                    mse.AddRange(ann.ErrorsLastTraining());

                    // Notification about training status
                    MseUpdate(this, mse);
                    if(outputs.Any())
                        OutputsUpdate(this, outputs);
                    if(weights.Any())
                        WeightsUpdate(this, weights);

                    Thread.Sleep(25);
                }
                catch(Exception e)
                {
                    logger.ErrorFormat(ExceptionManager.Parse(e) + Environment.NewLine + e.StackTrace);
                }
            }
            logger.InfoFormat("Stop network polling for notification");
        }
    }
}