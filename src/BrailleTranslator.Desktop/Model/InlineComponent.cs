using System;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class InlineComponent : Component
    {
        public InlineComponent()
        {
        }

        public InlineComponent(string title) : base(title)
        {
        }

        public InlineComponent(string title, Inline inline) : base(title)
        {
            if (inline == null) throw new ArgumentNullException(nameof(inline));

            Inline = inline;
        }

        public InlineComponent(Inline inline)
        {
            if (inline == null) throw new ArgumentNullException(nameof(inline));

            Inline = inline;
        }

        public Inline Inline { get; set; }

        public override void Delete()
        {
        }

        protected override void RemoveChild(Component component)
        {
            if (Inline.SiblingInlines.Count == 0) return;
        }
    }
}