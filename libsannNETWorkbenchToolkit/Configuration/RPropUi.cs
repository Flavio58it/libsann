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
    using libsannNETWorkbenchToolkit.Utils;

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
            rProp = NinjectBinding.GetKernel.Get<ResilientPropagation>();
            config = NinjectBinding.GetKernel.Get<ConfigurationFile>();

            maxUpdateValueNumericUpDown.Value = Convert.ToDecimal(rProp.MaxUpdateValue);
            minUpdateValueNumericUpDown.Value = Convert.ToDecimal(rProp.MinUpdateValue);
            growthFactorNumericUpDown.Value = Convert.ToDecimal(rProp.GrowthFactor);
            decreaseFactorNumericUpDown.Value = Convert.ToDecimal(rProp.DecreaseFactor);
        }

        private void saveClickHandler(object sender, EventArgs e)
        {
            rProp.MaxUpdateValue = Convert.ToDouble(maxUpdateValueNumericUpDown.Value);
            rProp.MinUpdateValue = Convert.ToDouble(minUpdateValueNumericUpDown.Value);
            rProp.GrowthFactor = Convert.ToDouble(growthFactorNumericUpDown.Value);
            rProp.DecreaseFactor = Convert.ToDouble(decreaseFactorNumericUpDown.Value);

            config.SaveRPropToFile();

            this.Dispose();
            this.Close();
        }
    }
}
