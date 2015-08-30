using System;
using System.Linq;
using System.Windows;

namespace BrailleTranslator.Desktop.Services
{
    public class WindowService : IWindowService
    {
        public void Open(string title, object dataContext, Action callback)
        {
            var window = new Window();

            window.Title = title;
            window.DataContext = dataContext;
            window.Content = dataContext;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ResizeMode = ResizeMode.NoResize;
            window.Topmost = true;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Closing += (s, e) => { callback?.Invoke(); };
            window.Show();
            window.Focus();
        }

        public void Close(object dataContext)
        {
            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(x => x.DataContext == dataContext)
                ?.Close();
        }
    }
}