using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsannNET;
using System.Xml.Serialization;

namespace libsannNETWorkbenchToolkit.Configuration
{
    [XmlType("BackPropagation")]
    public class BackPropagation
    {
        [XmlElement("LearningRate")]
        public double LearningRate { get; set; }
        [XmlElement("Beta")]
        public double Beta { get; set; }

        public BackPropagation()
        {
            // Default values
            LearningRate = 0.2;
            Beta = 0.2;
        }

        public void SetData(BackPropagation obj)
        {
            this.Beta = obj.Beta;
            this.LearningRate = obj.LearningRate;
        }
    }
}
