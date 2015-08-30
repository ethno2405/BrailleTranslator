using System;
using System.Windows.Input;
using BrailleTranslator.Desktop.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace BrailleTranslator.Desktop.Dialogs.ViewModels
{
    public class ComponentTitleViewModel : ViewModelBase
    {
        private string _title;

        private string _oldTitle;

        private IWindowService _windowService;

        public ComponentTitleViewModel(string title, IWindowService windowService)
        {
            if (windowService == null) throw new ArgumentNullException(nameof(windowService));

            _title = title;
            _oldTitle = title;

            _windowService = windowService;

            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand, CanCloseDialog);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);

            this.ForProperty(nameof(Title))
                .Subscribe(() => { ConfirmCommand.RaiseCanExecuteChanged(); });
        }

        public bool Result { get; set; }

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

        public RelayCommand ConfirmCommand { get; }

        public ICommand CancelCommand { get; }

        public void Close()
        {
            _windowService.Close(this);
        }

        private bool CanCloseDialog()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

        private void ExecuteConfirmCommand()
        {
            Result = true;
            Close();
        }

        private void ExecuteCancelCommand()
        {
            Result = false;
            Title = _oldTitle;
            Close();
        }
    }
}