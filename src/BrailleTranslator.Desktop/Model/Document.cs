using System;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.Model
{
    public class Document : ObservableObject
    {
        private string _name;

        private bool _isSaved;

        private FlowDocumentWrapper _flowDocument;

        public Document(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            _name = name;
            _isSaved = true;
            _flowDocument = new FlowDocumentWrapper(_name);
            _flowDocument.PropertyChanged += FlowDocumentPropertyChanged;
        }

        ~Document()
        {
            _flowDocument.PropertyChanged -= FlowDocumentPropertyChanged;
        }

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

        public bool IsSaved
        {
            get
            {
                return _isSaved;
            }
            set
            {
                Set(nameof(IsSaved), ref _isSaved, value);
            }
        }

        private void FlowDocumentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FlowDocument.Document))
            {
                IsSaved = false;
                RaisePropertyChanged(nameof(FlowDocument));
            }
        }
    }
}