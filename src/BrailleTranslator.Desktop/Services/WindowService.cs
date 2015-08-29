﻿using System;
using System.Linq;
using System.Windows;

namespace BrailleTranslator.Desktop.Services
{
    public class WindowService : IWindowService
    {
        public void Open(object dataContext, Action callback)
        {
            var window = new Window();

            window.DataContext = dataContext;
            window.Content = dataContext;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ResizeMode = ResizeMode.NoResize;
            window.Topmost = true;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Closing += (s, e) => { callback?.Invoke(); };
            window.ShowDialog();
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