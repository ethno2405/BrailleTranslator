using BrailleTranslator.Desktop.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Helpers
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel { get; } = SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}