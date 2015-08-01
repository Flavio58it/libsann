using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;

namespace libsannNETWorkbenchToolkit.Configuration
{
    using libsannNET;
    using libsannNETWorkbenchToolkit.Lib;
    using libsannNETWorkbenchToolkit.Utils;

    public partial class ModelOptionsUi : FormLoggable
    {
        protected AnnBuild annComp;
        protected ConfigurationFile config;

        public ModelOptionsUi()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            // Retrieve instances
            annComp = NinjectBinding.GetKernel.Get<AnnBuild>();
            config = NinjectBinding.GetKernel.Get<ConfigurationFile>();

            learningModeComboBox.Items.Add(Global.LearningDescription[Global.LearningMode.BACK_PROPAGATION]);
            learningModeComboBox.Items.Add(Global.LearningDescription[Global.LearningMode.RESILIENT_PROPAGATION]);

            transferFunctionComboBox.Items.Add(Global.ActivationDescription[ActivationNeuroMode.HYPERBOLIC_TANGENT]);
            transferFunctionComboBox.Items.Add(Global.ActivationDescription[ActivationNeuroMode.IDENTITY]);
            transferFunctionComboBox.Items.Add(Global.ActivationDescription[ActivationNeuroMode.SIGMOIDAL]);
            transferFunctionComboBox.Items.Add(Global.ActivationDescription[ActivationNeuroMode.STEP]);

            initWModeComboBox.Items.Add(Global.SynInitDescription[SynInitMode.NGUYEN_WINDROW]);
            initWModeComboBox.Items.Add(Global.SynInitDescription[SynInitMode.FAN_IN]);
            initWModeComboBox.Items.Add(Global.SynInitDescription[SynInitMode.NONE]);
            initWModeComboBox.Items.Add(Global.SynInitDescription[SynInitMode.POSITIVE_NEGATIVE_RANGE]);
            initWModeComboBox.Items.Add(Global.SynInitDescription[SynInitMode.POSITIVE_RANGE]);
            initWModeComboBox.Items.Add(Global.SynInitDescription[SynInitMode.ZERO]);

            learningModeComboBox.SelectedIndex = 0;
            transferFunctionComboBox.SelectedIndex = 0;
            initWModeComboBox.SelectedIndex = 0;

            inputUnitsNumericUpDown.Value = annComp.InputUnits;
            hiddenLayersNumericUpDown.Value = annComp.HiddenLayers;
            HiddenUnitsTextBox.Text = string.Empty;
            foreach(uint i in annComp.HiddenUnits)
                HiddenUnitsTextBox.Text += i.ToString() + ';';
            outputUnitsNumericUpDown.Value = annComp.OutputUnits;
            transferFunctionComboBox.SelectedIndex = transferFunctionComboBox.Items.IndexOf(Global.ActivationDescription[annComp.ActivationNeuroMode]);
            initWModeComboBox.SelectedIndex = initWModeComboBox.Items.IndexOf(Global.SynInitDescription[annComp.SynInitMode]);
            learningModeComboBox.SelectedIndex = learningModeComboBox.Items.IndexOf(Global.LearningDescription[annComp.LearningMode]);
            targetErrorNumericUpDown.Value = Convert.ToDecimal(annComp.ErrorTarget);
            epochsNumericUpDown.Value = annComp.MaxEpochs;
            useBiasCheckBox.Checked = annComp.Bias; 
        }

        private void saveClickHandler(object sender, EventArgs e)
        {
            annComp.Bias = useBiasCheckBox.Checked;
            annComp.ErrorTarget = Convert.ToDouble(targetErrorNumericUpDown.Value);
            annComp.HiddenLayers = Convert.ToUInt32(hiddenLayersNumericUpDown.Value);
            
            string[] hu = HiddenUnitsTextBox.Text.Split(';');

            if(hu.Count() > 0)
            {
                List<uint> l = new List<uint>();
                foreach(string s in hu)
                {
                    uint o;

                    if(uint.TryParse(s, out o))
                        l.Add(o);
                }
                annComp.HiddenUnits = l.ToArray();
            }

            annComp.InputUnits = Convert.ToUInt32(inputUnitsNumericUpDown.Value);
            annComp.LearningMode = Global.LearningDescription.FirstOrDefault(i => i.Value == learningModeComboBox.SelectedItem as string).Key;
            annComp.MaxEpochs = Convert.ToUInt32(epochsNumericUpDown.Value);
            annComp.ActivationNeuroMode = Global.ActivationDescription.FirstOrDefault(i => i.Value == transferFunctionComboBox.SelectedItem as string).Key;
            annComp.OutputUnits = Convert.ToUInt32(outputUnitsNumericUpDown.Value);
            annComp.SynInitMode = Global.SynInitDescription.FirstOrDefault(i => i.Value == initWModeComboBox.SelectedItem as string).Key;

            config.SaveAnnOptToFile();

            this.Dispose();
            this.Close();
        }

        private void LearningSetupClickHandler(object sender, EventArgs e)
        {
            switch(learningModeComboBox.SelectedItem as string)
            {
                case "Back-Propagation":
                    {
                        BackPropUi backPropUiConfig = new BackPropUi();
                        backPropUiConfig.ShowDialog(this);
                        break;
                    }
                case "Resilient-Propagation":
                    {
                        RPropUi RPropUiConfig = new RPropUi();
                        RPropUiConfig.ShowDialog(this);
                        break;
                    }
            }
        }
    }
}