using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Ninject;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace libsannNETWorkbenchToolkit
{
    using Set;
    using Utils;
    using Configuration;
    using ExceptionHandling;

    public partial class MainConsole : FormLoggable
    {
        protected ConfigurationFile config;
        protected AppCore core;

        protected enum AppStatus
        {
            READY,
            BUSY
        }

        public MainConsole()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            Setup();
        }

        protected void Setup()
        {
            try
            {
                logger.InfoFormat("Initial setup ...");

                config = NinjectBinding.GetKernel.Get<ConfigurationFile>();

                // Load last configurations
                config.LoadFromFile();

                // Core setup
                AppCoreSetup();
            }
            catch (Exception e)
            {
                ExceptionManager.LogAndShowException(e, "Error", logger);
            }
        }

        protected void AppCoreSetup()
        {
            // Build app core
            core = new AppCore();

            Action<Chart, object> action = UpdateChart;

            core.MseUpdate += (s, e) => Invoke(action, mse_chart, e);
            core.OutputsUpdate += (s, e) => Invoke(action, output_chart, e);
            core.WeightsUpdate += (s, e) => Invoke(action, weights_chart, e);
        }

        protected void UpdateChart(Chart chart, object values)
        {
            switch (chart.Name)
            {
                case "output_chart":
                {
                    List<double[][]> points = (values as List<double[][]>);
                    List<double> outputs = new List<double>();
                    List<double> target = new List<double>();

                    if (points != null)
                        foreach (double[][] data in points)
                        {
                            outputs.AddRange(data[0]);
                            target.AddRange(data[1]);
                        }

                    chart.Series[0].Points.DataBindY(outputs);
                    chart.Series[1].Points.DataBindY(target);

                    break;
                }
                default:
                {
                    List<double> points = (values as List<double>);
                    chart.Series[0].Points.DataBindY(points);
                    break;
                }
            }
        }

        protected void GuiBehavior(AppStatus status)
        {
            switch (status)
            {
                case AppStatus.BUSY:
                {
                    setupToolStripMenuItem.Enabled = false;
                    startWorkBenchToolStripMenuItem.Enabled = false;
                    exportToolStripMenuItem.Enabled = false;
                    break;
                }
                case AppStatus.READY:
                {
                    setupToolStripMenuItem.Enabled = true;
                    startWorkBenchToolStripMenuItem.Enabled = true;
                    exportToolStripMenuItem.Enabled = true;
                    break;
                }
            }
        }

        protected void ProcessingTrainingResult(TrainingResults result)
        {
            switch (result)
            {
                case TrainingResults.CONVERGENCE_NOT_REACHED:
                {
                    MessageBox.Show(this, "Convergence to precision target not reached.", "Training result",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                case TrainingResults.INTERNAL_ERROR:
                {
                    MessageBox.Show(this, "Internal error occurred.", "Training result", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }
                case TrainingResults.MODEL_INVALID:
                {
                    MessageBox.Show(this, "The data set is not valid.", "Training result", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;
                }
                case TrainingResults.NOT_VALIDATED:
                {
                    MessageBox.Show(this,
                        string.Format(
                            "Convergence has been reached. Network trained! But not validated, no validation data set present in the model.{0}Trained in {1} - Mse: {2}",
                            Environment.NewLine, core.TrainingEpoch(), core.LastMse().ToString("F5")), "Training result",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                case TrainingResults.TRAINING_SUCCESS:
                {
                    MessageBox.Show(this,
                        string.Format(
                            "Convergence has been reached. Network trained and validated!{0}Trained in {1} - Mse: {2}",
                            Environment.NewLine, core.TrainingEpoch(), core.LastMse().ToString("F5"), "Training result",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
                    break;
                }
                case TrainingResults.VALIDATION_FAIL:
                {
                    MessageBox.Show(this,
                        string.Format(
                            "Convergence has been reached but the network hasn't passed the validation test.{0}Trained in {1} - Mse: {2}",
                            Environment.NewLine, core.TrainingEpoch(), core.LastMse().ToString("F5")), "Training result",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
        }

        #region [ GUI handlers ]

        protected void toolStripMenuItemClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (string.Equals((sender as ToolStripMenuItem).Name, "generateExampleLayoutToolStripMenuItem"))
                {
                    var dialog = new SaveFileDialog();
                    dialog.Filter = "csv|*.csv";
                    dialog.Title = "Generate a set layout";
                    dialog.ShowDialog(this);

                    if (string.IsNullOrEmpty(dialog.FileName))
                        return;

                    var text = Export.GenerateLayout();

                    using (var w = new StreamWriter(dialog.FileName))
                    {
                        w.Write(text);
                    }
                }
                if (string.Equals((sender as ToolStripMenuItem).Name, "cSVToolStripMenuItem"))
                {
                    // Load data set from a csv file
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "csv|*.csv";
                    dialog.Title = "Set file";
                    dialog.ShowDialog();
                    string path = dialog.FileName;

                    if (string.IsNullOrWhiteSpace(path))
                        return;

                    GuiBehavior(AppStatus.BUSY);

                    int linesCount = SetLoader.LoadFromCsv(path);

                    GuiBehavior(AppStatus.READY);

                    logger.InfoFormat("Set loaded: {0}, read {1} lines", dialog.FileName, linesCount);
                }
                if (string.Equals((sender as ToolStripMenuItem).Name, "setToolStripMenuItem"))
                {
                    // Open set gui to view/edit the patterns
                    SetUi setUi = new SetUi();
                    setUi.ShowDialog(this);
                }
                if (string.Equals((sender as ToolStripMenuItem).Name, "configurationToolStripMenuItem"))
                {
                    // Open the options gui
                    ModelOptionsUi configGUI = new ModelOptionsUi();
                    configGUI.ShowDialog(this);

                    // Rebuild network
                    AppCoreSetup();
                }
                if (string.Equals((sender as ToolStripMenuItem).Name, "startWorkBenchToolStripMenuItem"))
                {
                    GuiBehavior(AppStatus.BUSY);

                    TrainingResults result = TrainingResults.INTERNAL_ERROR;

                    ThreadStart worker = delegate
                    {
                        // Run network training
                        result = core.Train(NinjectBinding.GetKernel.Get<SetModel>()).Result;
                    };

                    Thread waitTraining = new Thread(worker);
                    waitTraining.Start();

                    while (waitTraining.IsAlive)
                        Application.DoEvents();

                    ProcessingTrainingResult(result);

                    GuiBehavior(AppStatus.READY);
                }
                if (string.Equals((sender as ToolStripMenuItem).Name, "exitToolStripMenuItem"))
                {
                    // Dispose modules

                    // Before exit release the application resources
                    this.Dispose();
                    Application.Exit();
                }
                if (string.Equals((sender as ToolStripMenuItem).Name, "exportToolStripMenuItem"))
                {
                    var dialog = new SaveFileDialog();
                    dialog.Filter = "csv|*.csv";
                    dialog.Title = "Export serie to file";
                    dialog.ShowDialog(this);

                    if (string.IsNullOrEmpty(dialog.FileName))
                        return;

                    using (StreamWriter w = new StreamWriter(dialog.FileName))
                    {
                        switch (this.tabCtrl.SelectedIndex)
                        {
                            case 0:
                            {
                                Export.SeriesToFile(w, core.CurrentMse.ToArray());
                                break;
                            }
                            case 1:
                            {
                                Export.SeriesToFile(w, core.CurrentWeights.ToArray());
                                break;
                            }
                            case 2:
                            {
                                Export.TargetOutputsToFile(w, core.CurrentOutputs);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionManager.LogAndShowException(exception, "Error", logger);
            }
        }

        #endregion
    }
}