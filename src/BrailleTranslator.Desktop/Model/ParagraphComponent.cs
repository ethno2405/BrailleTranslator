using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class ParagraphComponent : BlockComponent
    {
        public ParagraphComponent()
        {
        }

        public ParagraphComponent(string title) : base(title)
        {
        }

        public ParagraphComponent(string title, Paragraph paragraph) : base(title, paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            foreach (var inline in paragraph.Inlines)
            {
                var child = ComponentFactory.CreateInlineCollection(inline);

                Children.Add(child);
            }
        }

        public ObservableCollection<InlineComponent> Children { get; } = new ObservableCollection<InlineComponent>();
    }
}