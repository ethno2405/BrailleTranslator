using System.Windows.Input;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class EditMenuViewModel : ViewModelBase
    {
        public EditMenuViewModel()
        {
            RegisterCommands();
        }

        public ICommand NewParagraphCommand { get; private set; }

        public ICommand NewSectionCommand { get; private set; }

        public ICommand NewVolumeCommand { get; private set; }

        private void RegisterCommands()
        {
            NewVolumeCommand = new RelayCommand(ExecuteNewVolumeCommand);
            NewSectionCommand = new RelayCommand(ExecuteNewSectionCommand);
            NewParagraphCommand = new RelayCommand(ExecuteNewParagraphCommand);
        }

        private void ExecuteNewVolumeCommand()
        {
            MessengerInstance.Send(new KeyShortcutMessage(Key.Enter, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt));
        }

        private void ExecuteNewSectionCommand()
        {
            MessengerInstance.Send(new KeyShortcutMessage(Key.Enter, ModifierKeys.Control | ModifierKeys.Shift));
        }

        private void ExecuteNewParagraphCommand()
        {
            MessengerInstance.Send(new KeyShortcutMessage(Key.Enter, ModifierKeys.Control));
        }
    }
}