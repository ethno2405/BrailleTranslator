using System;
using System.Windows;
using BrailleTranslator.Desktop.ViewModels;
using BrailleTranslator.Desktop.Views;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Helpers
{
    public class ViewModelLocator
    {
        public ViewModelLocator(IMapper mapper)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            mapper.Map<ToolbarViewModel, ToolbarView>()
                  .Map<MainContentViewModel, MainContentView>();

            Application.Current.Startup += ApplicationStartup;
        }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            Application.Current.MainWindow.DataContext = SimpleIoc.Default.GetInstance<MainWindowViewModel>();
        }
    }
}