using System;
using System.Globalization;
using System.Windows.Data;

namespace BrailleTranslator.Desktop.Converters
{
    public class RootTreeViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object[] { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}