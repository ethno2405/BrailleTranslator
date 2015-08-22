using System.Windows;
using BrailleTranslator.Desktop.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SimpleIoc.Default.Register<ToolbarViewModel>();
            SimpleIoc.Default.Register<MainContentViewModel>();
            SimpleIoc.Default.Register(c =>
            {
                return new MainViewModel(c.GetInstance<ToolbarViewModel>(), c.GetInstance<MainContentViewModel>());
            });
        }
    }
}