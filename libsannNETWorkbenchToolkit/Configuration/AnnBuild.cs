using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsannNET;
using System.Xml.Serialization;

namespace libsannNETWorkbenchToolkit.Lib
{
    using libsannNETWorkbenchToolkit.Configuration;
    using libsannNETWorkbenchToolkit.Utils;

    [XmlType("Ann")]
    public class AnnBuild
    {
        [XmlElement("InputUnits")]
        public uint InputUnits { get; set; }
        [XmlArrayItem("HiddenUnits")]
        public uint[] HiddenUnits { get; set; }
        [XmlElement("HiddenLayers")]
        public uint HiddenLayers { get; set; }
        [XmlElement("OutputUnits")]
        public uint OutputUnits { get; set; }
        [XmlElement("UseBias")]
        public bool Bias { get; set; }
        [XmlElement("ActivationMode")]
        public ActivationNeuroMode ActivationNeuroMode { get; set; }
        [XmlElement("SynInitMode")]
        public SynInitMode SynInitMode { get; set; }
        [XmlElement("LearningMode")]
        public Global.LearningMode LearningMode { get; set; }
        [XmlElement("ErrorTarget")]
        public double ErrorTarget { get; set; }
        [XmlElement("MaxEpochs")]
        public uint MaxEpochs { get; set; }

        public AnnBuild()
        {
            // Default values
            InputUnits = 2;
            HiddenUnits = new uint[2] {2, 2};
            HiddenLayers = 2;
            OutputUnits = 1;
            Bias = true;
            ActivationNeuroMode = ActivationNeuroMode.HYPERBOLIC_TANGENT;
            SynInitMode = SynInitMode.NGUYEN_WINDROW;
            LearningMode = Global.LearningMode.RESILIENT_PROPAGATION;
            ErrorTarget = 0.01;
            MaxEpochs = 10000;
        }

        public void SetData(AnnBuild obj)
        {
            this.Bias = obj.Bias;
            this.ErrorTarget = obj.ErrorTarget;
            this.HiddenLayers = obj.HiddenLayers;
            this.HiddenUnits = obj.HiddenUnits;
            this.InputUnits = obj.InputUnits;
            this.LearningMode = obj.LearningMode;
            this.MaxEpochs = obj.MaxEpochs;
            this.ActivationNeuroMode = obj.ActivationNeuroMode;
            this.OutputUnits = obj.OutputUnits;
            this.SynInitMode = obj.SynInitMode;
        }
    }
}
