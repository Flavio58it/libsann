using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libsannNETWorkbenchToolkit.Set
{
    public class SetModel
    {
        public double[][] inMatrix { get; set; }
        public double[][] outMatrix { get; set; }
        public double[][] inValMatrix { get; set; }
        public double[][] outValMatrix { get; set; }

        public SetModel()
        { }

        public bool IsValid
        {
            get
            {
                if(
                    inMatrix != null && inMatrix.Count() > 0 &&
                    outMatrix != null && outMatrix.Count() > 0 &&
                    inMatrix[0] != null && inMatrix[0].Count() > 0 &&
                    outMatrix[0] != null && outMatrix[0].Count() > 0 &&
                    inMatrix.Count() == outMatrix.Count()
                    )
                    return true;
                return false;
            }
        }

        public bool HasValidationData
        {
            get
            {
                if(inValMatrix != null && inValMatrix.Count() > 0 && inValMatrix[0] != null && inValMatrix[0].Count() > 0 &&
                    outValMatrix != null && outValMatrix.Count() > 0 && outValMatrix[0] != null && outValMatrix[0].Count() > 0)
                    return true;
                return false;
            }
        }
    }
}
