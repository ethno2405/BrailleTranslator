using System;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class RunComponent : InlineComponent
    {
        public RunComponent()
        {
        }

        public RunComponent(string title) : base(title)
        {
        }

        public RunComponent(string title, Run run) : base(title, run)
        {
        }

        public RunComponent(Run run) : base(run)
        {
        }

        public string Text
        {
            get
            {
                return (Inline as Run).Text;
            }
        }

        public override bool IsVisible
        {
            get
            {
                return false;
            }
        }

        protected override void RemoveChild(Component component)
        {
            throw new NotSupportedException("Run component does not have child components");
        }

        protected override void PopulateChildren(TextElement textElement)
        {
            var run = textElement as Run;

            if (run == null) throw new ArgumentException(string.Concat("Text element is not of type ", GetType().FullName), nameof(textElement));

            Inline = run;
        }
    }
}