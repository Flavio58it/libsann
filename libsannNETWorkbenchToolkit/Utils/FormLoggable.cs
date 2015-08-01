using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libsannNETWorkbenchToolkit.Utils
{
    public class FormLoggable : Form
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(Program));        
    }
}
