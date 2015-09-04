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

        private ITranslationService _translationService;

        public MainContentViewModel(IWindowService windowService, ITranslationService translationService)
        {
            if (windowService == null) throw new ArgumentNullException(nameof(windowService));
            if (translationService == null) throw new ArgumentNullException(nameof(translationService));

            _windowService = windowService;
            _translationService = translationService;

            _project = new Project();
            _project.CreateDocument("Document");

            MessengerInstance.Register<NotificationMessageAction<string>>(this, Tokens.NewComponent, m => OpenNewComponentDialog(m.Notification, m.Execute));
            MessengerInstance.Register<GenericMessage<string>>(this, Tokens.Translation, m =>
            {
                var result = _translationService.TranslateToBraille(m.Content);
                _project.CurrentDocument.FlowDocument.Preview = result;
            });
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