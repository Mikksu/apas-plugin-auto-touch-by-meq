using System.Collections.ObjectModel;
using APAS.Plugin.AutoTouchByMEQ.Classes;
using APAS.ServiceContract.Wcf;

namespace APAS.Plugin.AutoTouchByMEQ.ViewModels
{
    public class AxisInfoManager
    {
        #region Variables

        #endregion

        #region Ctors

        public AxisInfoManager(ISystemService service, Configuration config)
        {
            AxisCollection = new ObservableCollection<AxisInfo>();

            config.AxisCollection.ForEach(cfgAxis =>
            {
                AxisCollection.Add(new AxisInfo(service, cfgAxis.Lmc1, cfgAxis.Axis1, cfgAxis.Lmc2, cfgAxis.Axis2,
                    cfgAxis.CcwButtonCaption, cfgAxis.CwButtonCaption, 
                    cfgAxis.IsSwapAxis1Dir, cfgAxis.IsSwapAxis2Dir));
            });
        }

        #endregion

        #region Properties

        public ObservableCollection<AxisInfo> AxisCollection { get; }

        #endregion
    }
}
