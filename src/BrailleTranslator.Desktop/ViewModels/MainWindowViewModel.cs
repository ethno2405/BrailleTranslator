using System;
using System.Windows.Input;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(object toolbarViewModel, object mainContentViewModel)
        {
            if (toolbarViewModel == null) throw new ArgumentNullException(nameof(toolbarViewModel));
            if (mainContentViewModel == null) throw new ArgumentNullException(nameof(mainContentViewModel));

            ToolbarViewModel = toolbarViewModel;
            MainContentViewModel = mainContentViewModel;
            PublishKeyShortcutMessageCommand = new RelayCommand<KeyEventArgs>(PublishKeyShortcutMessage);
        }

        public object ToolbarViewModel { get; private set; }

        public object MainContentViewModel { get; private set; }

        public ICommand PublishKeyShortcutMessageCommand { get; }

        private void PublishKeyShortcutMessage(KeyEventArgs e)
        {
            var message = new KeyShortcutMessage(e.Key, e.KeyboardDevice.Modifiers);
            var token = KeyShortcutMessageToken.Create(e.Key, e.KeyboardDevice.Modifiers);

            Messenger.Default.Send(message, token);
        }
    }
}