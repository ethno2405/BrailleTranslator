using System;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(ToolbarViewModel toolbarViewModel, MainContentViewModel mainContentViewModel)
        {
            if (toolbarViewModel == null) throw new ArgumentNullException(nameof(toolbarViewModel));
            if (mainContentViewModel == null) throw new ArgumentNullException(nameof(mainContentViewModel));

            ToolbarViewModel = toolbarViewModel;
            MainContentViewModel = mainContentViewModel;
        }

        public ToolbarViewModel ToolbarViewModel { get; private set; }

        public MainContentViewModel MainContentViewModel { get; private set; }
    }
}