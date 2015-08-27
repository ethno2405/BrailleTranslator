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
            if (paragraph.Inlines.FirstInline != null)
            {
                InlineComponent = ComponentFactory.CreateInlineCollection(paragraph.Inlines.FirstInline);
                InlineComponent.Parent = this;
            }
        }

        public InlineComponent InlineComponent { get; set; }
    }
}