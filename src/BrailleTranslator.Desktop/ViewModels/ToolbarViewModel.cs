using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class ToolbarViewModel : ViewModelBase
    {
        public static ICommand OpenCommand { get; set; }
        public static ICommand SaveCommand { get; set; }
        public static ICommand PrintCommand { get; set; }
        public static ICommand ExitCommand { get; set; }

        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                RaisePropertyChanged("SelectedPath");
            }
        }

        private string _defaultPath;

        public ToolbarViewModel()
        {
            RegisterCommands();
        }
        /*
        public ToolbarViewModel(string defaultPath)
        {
            _defaultPath = defaultPath;
            RegisterCommands();
        }*/

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
            SaveCommand = new RelayCommand(ExecuteSaveFileDialog);
            PrintCommand = new RelayCommand(ExecutePrintFileDialog);
            ExitCommand = new RelayCommand(ExecuteExitDialog);
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
        }

        private void ExecuteSaveFileDialog()
        {
            var dialog = new SaveFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
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