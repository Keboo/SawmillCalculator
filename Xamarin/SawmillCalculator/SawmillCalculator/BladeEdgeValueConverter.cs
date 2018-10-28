using System;
using System.Globalization;
using Xamarin.Forms;

namespace SawmillCalculator
{
    public class BladeEdgeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Bottom or Right" : "Top or Left";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}