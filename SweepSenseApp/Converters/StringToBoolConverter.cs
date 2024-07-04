using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SweepSenseApp.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                var parameters = parameter?.ToString().Split(',');
                return boolValue ? parameters[1] : parameters[0];
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
