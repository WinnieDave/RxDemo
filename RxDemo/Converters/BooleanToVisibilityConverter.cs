using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RxDemo.Converters
{
    class BooleanToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var toBoolean = (bool)value;
            return toBoolean ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
