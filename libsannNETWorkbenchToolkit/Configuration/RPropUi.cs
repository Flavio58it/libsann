using System;
using Ninject;

namespace libsannNETWorkbenchToolkit.Configuration
{
    using ExceptionHandling;
    using Utils;

    public partial class RPropUi : FormLoggable
    {
        protected ResilientPropagation rProp;
        protected ConfigurationFile config;

        public RPropUi()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            try
            {
                rProp = NinjectBinding.GetKernel.Get<ResilientPropagation>();
                config = NinjectBinding.GetKernel.Get<ConfigurationFile>();

                maxUpdateValueNumericUpDown.Value = Convert.ToDecimal(rProp.MaxUpdateValue);
                minUpdateValueNumericUpDown.Value = Convert.ToDecimal(rProp.MinUpdateValue);
                growthFactorNumericUpDown.Value = Convert.ToDecimal(rProp.GrowthFactor);
                decreaseFactorNumericUpDown.Value = Convert.ToDecimal(rProp.DecreaseFactor);
            }
            catch (Exception e)
            {
                ExceptionManager.LogAndShowException(e, "Error", logger);
            }
        }

        private void saveClickHandler(object sender, EventArgs e)
        {
            try
            {
                rProp.MaxUpdateValue = Convert.ToDouble(maxUpdateValueNumericUpDown.Value);
                rProp.MinUpdateValue = Convert.ToDouble(minUpdateValueNumericUpDown.Value);
                rProp.GrowthFactor = Convert.ToDouble(growthFactorNumericUpDown.Value);
                rProp.DecreaseFactor = Convert.ToDouble(decreaseFactorNumericUpDown.Value);

                config.SaveRPropToFile();

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
