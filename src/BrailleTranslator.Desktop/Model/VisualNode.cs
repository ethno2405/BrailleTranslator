using System;
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

        private string _title;

        private bool _isEditing;

        public VisualNode()
        {
            _title = GetType().Name;

            RegisterCommands();
        }

        public VisualNode(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            _title = title;

            RegisterCommands();

            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.F2, ModifierKeys.None), m =>
            {
                if (IsSelected)
                {
                    ExecuteRenameCommand();
                }
            });
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

        public virtual bool IsEditable
        {
            get
            {
                return true;
            }
        }

        public bool IsEditing
        {
            get
            {
                return _isEditing;
            }
            set
            {
                Set(nameof(IsEditing), ref _isEditing, value);
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

        private void RegisterCommands()
        {
            RenameCommand = new RelayCommand(ExecuteRenameCommand);
        }

        private void ExecuteRenameCommand()
        {
            IsEditing = true;

            var currentTitle = Title;

            this.ForProperty(nameof(Title))
                .AndProperty(nameof(IsEditing))
                .When(() => !IsEditing && string.IsNullOrEmpty(Title))
                .Subscribe(() => Title = currentTitle)
                .DisposeWhen(() => !IsEditing && !string.IsNullOrEmpty(Title));
        }
    }
}