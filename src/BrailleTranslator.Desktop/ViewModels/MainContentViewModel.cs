using BrailleTranslator.Desktop.Model;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class MainContentViewModel : ViewModelBase
    {
        private Project _project;

        public MainContentViewModel()
        {
            _project = new Project();
            _project.CreateDocument("Document");
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
    }
}