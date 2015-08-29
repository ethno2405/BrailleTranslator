using System.Windows;
using BrailleTranslator.Desktop.Helpers;
using BrailleTranslator.Desktop.Services;
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
            SimpleIoc.Default.Register<ToolbarViewModel>();
            SimpleIoc.Default.Register<IMapper, Mapper>();
            SimpleIoc.Default.Register<IWindowService, WindowService>();
            SimpleIoc.Default.Register<IComponentFactory, ComponentFactory>();

            SimpleIoc.Default.Register(c =>
            {
                return new MainWindowViewModel(c.GetInstance<ToolbarViewModel>(), c.GetInstance<MainContentViewModel>());
            })
            .Register(c =>
            {
                return new ViewModelLocator(c.GetInstance<IMapper>());
            })
            .Register(c =>
            {
                return new MainContentViewModel(c.GetInstance<IWindowService>());
            });

            Resources.Add("ViewModelLocator", SimpleIoc.Default.GetInstance<ViewModelLocator>());

            MainWindow = new Views.MainWindow();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}