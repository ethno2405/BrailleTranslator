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
            InitializeInlineComponent(paragraph);
        }

        public ParagraphComponent(Paragraph paragraph) : base(paragraph)
        {
            InitializeInlineComponent(paragraph);
        }

        public InlineComponent InlineComponent { get; set; }

        private void InitializeInlineComponent(Paragraph paragraph)
        {
            if (paragraph.Inlines.FirstInline != null)
            {
                InlineComponent = ComponentFactory.CreateComponent(paragraph.Inlines.FirstInline) as InlineComponent;
                InlineComponent.Parent = this;
            }
        }
    }
}