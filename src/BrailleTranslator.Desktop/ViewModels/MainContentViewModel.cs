using System;
using BrailleTranslator.Desktop.Dialogs.ViewModels;
using BrailleTranslator.Desktop.Messages;
using BrailleTranslator.Desktop.Model;
using BrailleTranslator.Desktop.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class MainContentViewModel : ViewModelBase
    {
        private Project _project;

        private IWindowService _windowService;

        public MainContentViewModel(IWindowService windowService)
        {
            if (windowService == null) throw new ArgumentNullException(nameof(windowService));

            _windowService = windowService;

            _project = new Project();
            _project.CreateDocument("Document");

            MessengerInstance.Register<NotificationMessageAction<string>>(this, Tokens.NewComponent, m => OpenNewComponentDialog(m.Notification, m.Execute));
        }

        public Project Project
        {
            get
            {
                return _project;
            }
            set
            {
                Set(nameof(Project), ref _project, value);
            }
        }

        private void OpenNewComponentDialog(string dialogTitle, Action<string> callback)
        {
            OpenComponentTitleDialog(dialogTitle, string.Empty, callback);
        }

        private void OpenComponentTitleDialog(string dialogTitle, string currentTitle, Action<string> callback)
        {
            var viewModel = new ComponentTitleViewModel(currentTitle, _windowService);
            _windowService.Open(dialogTitle, viewModel, () =>
            {
                if (viewModel.Result)
                {
                    callback?.Invoke(viewModel.Title);
                }
            });
        }
    }
}