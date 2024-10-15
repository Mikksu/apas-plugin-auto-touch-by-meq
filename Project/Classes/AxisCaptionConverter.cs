using System;
using System.Globalization;
using System.Windows.Data;

namespace APAS.Plugin.AutoTouchByMEQ.Classes
{
    public class AxisCaptionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // value[0] is LMC name.
            // value[1] is Axis name.

            return $"{values[0]} - {values[1]}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
