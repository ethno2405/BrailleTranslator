using System;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class ToolbarViewModel : ViewModelBase
    {
        public ToolbarViewModel(FileMenuViewModel fileMenuViewModel, EditMenuViewModel editMenuViewModel)
        {
            if (fileMenuViewModel == null) throw new ArgumentNullException(nameof(fileMenuViewModel));
            if (editMenuViewModel == null) throw new ArgumentNullException(nameof(editMenuViewModel));

            FileMenuViewModel = fileMenuViewModel;
            EditMenuViewModel = editMenuViewModel;
        }

        public FileMenuViewModel FileMenuViewModel { get; }

        public EditMenuViewModel EditMenuViewModel { get; }
    }
}