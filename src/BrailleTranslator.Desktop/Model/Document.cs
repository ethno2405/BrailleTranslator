using System;
using System.Windows.Documents;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.Model
{
    public class Document : ObservableObject
    {
        private string _name;

        private bool _isSaved;

        private FlowDocument _flowDocument;

        public Document(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            _name = name;
            _isSaved = true;
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

        public FlowDocument FlowDocument
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
    }
}