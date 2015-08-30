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

        public static PropertyChangedHandler ForProperty(this INotifyPropertyChanged propChanged, INotifyPropertyChanged observable, string propertyName)
        {
            return new PropertyChangedHandler(observable, propertyName);
        }
    }
}