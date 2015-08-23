using System;

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
        }

        public object ToolbarViewModel { get; private set; }

        public object MainContentViewModel { get; private set; }
    }
}