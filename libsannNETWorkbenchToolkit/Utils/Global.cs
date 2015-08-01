using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsannNET;

namespace libsannNETWorkbenchToolkit.Utils
{
    public static class Global
    {
        public enum LearningMode
        {
            BACK_PROPAGATION,
            RESILIENT_PROPAGATION
        }

        public static readonly Dictionary<LearningMode, string> LearningDescription = new Dictionary<LearningMode, string>()
        {
            { LearningMode.BACK_PROPAGATION, "Back-Propagation" },
            { LearningMode.RESILIENT_PROPAGATION, "Resilient-Propagation" }
        };

        public static readonly Dictionary<ActivationNeuroMode, string> ActivationDescription = new Dictionary<ActivationNeuroMode, string>()
        {
            { ActivationNeuroMode.HYPERBOLIC_TANGENT, "Hyperbolic tangent" },
            { ActivationNeuroMode.IDENTITY, "Identity" },
            { ActivationNeuroMode.SIGMOIDAL, "Sigmoidal" },
            { ActivationNeuroMode.STEP, "Step" }
        };

        public static readonly Dictionary<SynInitMode, string> SynInitDescription = new Dictionary<SynInitMode, string>()
        {
            { SynInitMode.FAN_IN, "Fan In" },
            { SynInitMode.NGUYEN_WINDROW, "Nguyen Windrow" },
            { SynInitMode.NONE, "None" },
            { SynInitMode.POSITIVE_NEGATIVE_RANGE, "Random positive-negative range"},
            { SynInitMode.POSITIVE_RANGE, "Random positive range" },
            { SynInitMode.ZERO, "Zero" }
        };
    }
}
