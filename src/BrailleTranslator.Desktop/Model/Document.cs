using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace BrailleTranslator.Desktop.Model
{
    public class Document : ObservableObject
    {
        private string _name;

        private bool _isDirty;

        private FlowDocumentWrapper _flowDocument;

        public Document(string name, Project parent)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            _name = name;
            _isDirty = true;
            _flowDocument = new FlowDocumentWrapper(_name);
            _flowDocument.PropertyChanged += FlowDocumentPropertyChanged;
            CloseDocumentCommand = new RelayCommand(CloseDocument);
            Parent = parent;
        }

        ~Document()
        {
            _flowDocument.PropertyChanged -= FlowDocumentPropertyChanged;
        }

        public ICommand CloseDocumentCommand { get; }
        public Project Parent { get; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(nameof(Name), ref _name, value);
            }
        }

        public FlowDocumentWrapper FlowDocument
        {
            get
            {
                return _flowDocument;
            }
            set
            {
                Set(nameof(FlowDocument), ref _flowDocument, value);
            }
        }

        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                Set(nameof(IsDirty), ref _isDirty, value);
            }
        }

        private void CloseDocument()
        {
            Parent.CloseDocument(this);
        }

        private void FlowDocumentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FlowDocument.Document))
            {
                IsDirty = true;
                RaisePropertyChanged(nameof(FlowDocument));
            }
        }
    }
}