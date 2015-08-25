using System;
using System.Collections.ObjectModel;

namespace BrailleTranslator.Desktop.Model
{
    public class DocumentRoot
    {
        public DocumentRoot(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            Title = title;
            Blocks = new ObservableCollection<DocumentBlock>();
        }

        public string Title { get; set; }

        public ObservableCollection<DocumentBlock> Blocks { get; }
    }
}