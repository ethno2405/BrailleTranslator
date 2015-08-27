using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class SectionComponent : BlockComponent
    {
        public SectionComponent()
        {
        }

        public SectionComponent(string title) : base(title)
        {
        }

        public SectionComponent(string title, Section section) : base(title, section)
        {
            PopulateChildren(section.Blocks);
        }

        public SectionComponent(Section section) : base(section)
        {
            PopulateChildren(section.Blocks);
        }
    }
}