using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libsannNETWorkbenchToolkit.Utils
{
    public abstract class ObjectLoggable
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(Program));
    }
}
