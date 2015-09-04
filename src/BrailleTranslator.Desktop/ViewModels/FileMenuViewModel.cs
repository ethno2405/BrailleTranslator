using BrailleTranslator.Desktop.Messages;
using BrailleTranslator.Desktop.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class FileMenuViewModel : ViewModelBase
    {
        private IFileService _fileService;

        private string _selectedPath;

        private string _defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public FileMenuViewModel(IFileService fileService)
        {
            if (fileService == null) throw new ArgumentNullException(nameof(fileService));

            _fileService = fileService;

            RegisterCommands();
            RegisterForMessages();
        }

        public ICommand OpenCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand PrintCommand { get; private set; }

        public ICommand ExitCommand { get; private set; }

        public string SelectedPath
        {
            get
            {
                return _selectedPath;
            }
            set
            {
                Set(nameof(SelectedPath), ref _selectedPath, value);
            }
        }

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
            SaveCommand = new RelayCommand(ExecuteSaveFileDialog);
            PrintCommand = new RelayCommand(ExecutePrintFileDialog);
            ExitCommand = new RelayCommand(ExecuteExitDialog);
        }

        private void RegisterForMessages()
        {
            MessengerInstance.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.O, ModifierKeys.Control), (m) => ExecuteOpenFileDialog());
            MessengerInstance.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.P, ModifierKeys.Control), (m) => ExecutePrintFileDialog());
            MessengerInstance.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.S, ModifierKeys.Control | ModifierKeys.Shift), (m) => ExecuteSaveFileDialog());
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };

            dialog.Filter = "Braille Files (*.brf)|*.brf|Text Files (*.txt)|*.txt";

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                SelectedPath = dialog.FileName;
                var fileAsString = _fileService.Open(SelectedPath);
            }
        }

        private void ExecuteSaveFileDialog()
        {
            var dialog = new SaveFileDialog { InitialDirectory = _defaultPath };

            dialog.Filter = "Braille Files (*.brf)|*.brf";
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                SelectedPath = dialog.FileName;
                var content = CheckFileValidation();
                _fileService.Save(SelectedPath, content);
            }
        }

        private string CheckFileValidation()
        {
            return string.Empty;
        }

        private void ExecutePrintFileDialog()
        {
            var dialog = new PrintDialog();

            dialog.ShowDialog();
        }

        private void ExecuteExitDialog()
        {
            Environment.Exit(0);
        }
    }
}