using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.Model
{
    public class Project : ObservableObject
    {
        private Document _currentDocument;

        public ObservableCollection<Document> Documents { get; } = new ObservableCollection<Document>();

        public Document CurrentDocument
        {
            get
            {
                return _currentDocument;
            }
            set
            {
                Set(nameof(CurrentDocument), ref _currentDocument, value);
            }
        }

        public void CreateDocument(string name)
        {
            var newDocument = new Document(name, this);
            Documents.Add(newDocument);
            CurrentDocument = newDocument;
        }

        public void CloseDocument(Document document)
        {
            if (document.IsDirty)
            {
                // TODO: ask for user to save
            }

            var documentIndex = Documents.IndexOf(document);
            Documents.Remove(document);
        }
    }
}