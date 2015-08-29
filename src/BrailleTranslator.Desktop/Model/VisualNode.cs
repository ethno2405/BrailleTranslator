using System;
using System.Windows;
using System.Windows.Input;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class VisualNode : ObservableObject
    {
        private bool _isSelected;

        private bool _isExpanded = true;

        private bool _isFocused;

        private string _title;

        public VisualNode()
        {
            _title = GetType().Name;

            RegisterCommands();
            SubscribeForMessages();
        }

        public VisualNode(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            _title = title;

            RegisterCommands();
            SubscribeForMessages();
        }

        public ICommand RenameCommand { get; private set; }

        public virtual bool IsVisible { get; } = true;

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                Set(nameof(IsExpanded), ref _isExpanded, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                Set(nameof(IsSelected), ref _isSelected, value);
            }
        }

        public bool IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                if (Set(nameof(IsFocused), ref _isFocused, value))
                {
                    MessageBox.Show(_isFocused.ToString());
                }
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(nameof(Title), ref _title, value);
            }
        }

        private void SubscribeForMessages()
        {
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.F2, ModifierKeys.None), m =>
            {
                if (IsSelected)
                {
                    ExecuteRenameCommand();
                }
            });
        }

        private void RegisterCommands()
        {
            RenameCommand = new RelayCommand(ExecuteRenameCommand);
        }

        private void ExecuteRenameCommand()
        {
            var message = new NotificationMessageAction<string>(Title, t => Title = t);

            Messenger.Default.Send(message, Tokens.Rename);
        }
    }
}