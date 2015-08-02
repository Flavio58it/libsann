using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libsannNETWorkbenchToolkit.ExceptionHandling;
using Ninject;

namespace libsannNETWorkbenchToolkit.Configuration
{
    using libsannNETWorkbenchToolkit.Utils;

    public partial class BackPropUi : FormLoggable
    {
        protected BackPropagation backPropComp;
        protected ConfigurationFile config;

        public BackPropUi()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            try
            {
                backPropComp = NinjectBinding.GetKernel.Get<BackPropagation>();
                config = NinjectBinding.GetKernel.Get<ConfigurationFile>();

                momentumNumericUpDown.Value = Convert.ToDecimal(backPropComp.Beta);
                learningRateNumericUpDown.Value = Convert.ToDecimal(backPropComp.LearningRate);
            }
            catch (Exception exception)
            {
                ExceptionManager.LogAndShowException(exception, "Error", logger);
            }
        }

        private void saveClickHandler(object sender, EventArgs e)
        {
            try
            {
                backPropComp.Beta = Convert.ToDouble(momentumNumericUpDown.Value);
                backPropComp.LearningRate = Convert.ToDouble(learningRateNumericUpDown.Value);

                config.SaveBackPropToFile();

                this.Dispose();
                this.Close();
            }
            catch (Exception exception)
            {
                ExceptionManager.LogAndShowException(exception, "Error", logger);
            }
        }
    }
}
