using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BrailleTranslator.Desktop.Converters
{
    public class BoolValueVisibilityConverter : IValueConverter
    {
        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;

            return visibility == Visibility.Visible;
        }
    }
}