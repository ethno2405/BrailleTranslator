using System.ComponentModel;
using BrailleTranslator.Desktop.Helpers;

namespace GalaSoft.MvvmLight
{
    public static class ObservableObjectExtensions
    {
        public static PropertyChangedHandler ForProperty(this INotifyPropertyChanged propChanged, string propertyName)
        {
            return new PropertyChangedHandler(propChanged, propertyName);
        }
    }
}