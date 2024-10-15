using System.Collections.Generic;

namespace APAS.Plugin.AutoTouchByMEQ.Classes
{
    public class Configuration
    {
        public Configuration()
        {
            AxisCollection = new List<AxisConfig>();
        }

        public List<AxisConfig> AxisCollection { get; }
    }

    public class AxisConfig
    {
        public string Lmc1 { get; set; }

        public string Axis1 { get; set; }

        public string Lmc2 { get; set; }

        public string Axis2 { get; set; }

        public string CcwButtonCaption { get; set; }

        public string CwButtonCaption { get; set; }

        public bool IsSwapAxis1Dir { get; set; }

        public bool IsSwapAxis2Dir { get; set; }
    }
}
