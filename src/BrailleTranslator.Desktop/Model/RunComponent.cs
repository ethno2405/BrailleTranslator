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

        public string Text { get; set; }
    }
}