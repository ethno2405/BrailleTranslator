using System;
using System.Collections.ObjectModel;

namespace BrailleTranslator.Desktop.Model
{
    public class DocumentBlock
    {
        public DocumentBlock(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            Title = title;
            Children = new ObservableCollection<DocumentBlock>();
        }

        public string Title { get; set; }

        public ObservableCollection<DocumentBlock> Children { get; }

        public DocumentBlock AddChild(DocumentBlock child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (Children.Contains(child)) throw new ArgumentException("The element is already a child", nameof(child));

            Children.Add(child);

            return this;
        }
    }
}