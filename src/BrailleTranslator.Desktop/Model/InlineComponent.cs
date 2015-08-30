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

        protected override TextElement Payload
        {
            get
            {
                return Inline;
            }
            set
            {
                Inline = (Inline)value;
            }
        }

        public override void Delete()
        {
        }

        protected override bool CanDelete()
        {
            return false;
        }

        protected override bool CanMoveUp()
        {
            return false;
        }

        protected override bool CanMoveDown()
        {
            return false;
        }
    }
}