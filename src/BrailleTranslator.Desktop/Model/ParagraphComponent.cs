using System;
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

        protected Paragraph Paragraph
        {
            get
            {
                return Block as Paragraph;
            }
            set
            {
                Block = value;
            }
        }

        protected override void RemoveChild(Component component)
        {
            var inlineComponent = component as InlineComponent;

            if (inlineComponent == null) throw new InvalidOperationException(string.Format("Component is of type {0}, but {1} is expected.", component.GetType(), typeof(InlineComponent)));

            Paragraph.Inlines.Remove(inlineComponent.Inline);

            InlineComponent = null;
        }

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