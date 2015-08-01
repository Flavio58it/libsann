using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsannNET;
using System.Xml.Serialization;

namespace libsannNETWorkbenchToolkit.Configuration
{
    [XmlType("ResilientPropagation")]
    public class ResilientPropagation
    {
        [XmlElement("MaxUpdate")]
        public double MaxUpdateValue { get; set; }
        [XmlElement("MinUpdate")]
        public double MinUpdateValue { get; set; }
        [XmlElement("GowthFactor")]
        public double GrowthFactor { get; set; }
        [XmlElement("DecreaseFactor")]
        public double DecreaseFactor { get; set; }

        public ResilientPropagation()
        {
            // Default values
            MaxUpdateValue = 50.0;
            MinUpdateValue = 0.001;
            GrowthFactor = 1.2;
            DecreaseFactor = 0.5;
        }

        public void SetData(ResilientPropagation obj)
        {
            this.DecreaseFactor = obj.DecreaseFactor;
            this.GrowthFactor = obj.GrowthFactor;
            this.MaxUpdateValue = obj.MaxUpdateValue;
            this.MinUpdateValue = obj.MinUpdateValue;
        }
    }
}
