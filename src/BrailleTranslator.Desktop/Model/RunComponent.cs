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
            Text = run.Text;
        }

        public RunComponent(Run run) : base(run)
        {
            Text = run.Text;
        }

        public string Text { get; set; }

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
    }
}