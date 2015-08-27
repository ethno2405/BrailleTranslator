using System;
using System.Collections.ObjectModel;
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
            foreach (var block in section.Blocks)
            {
                var child = ComponentFactory.CreateBlockComponent(block);

                Childred.Add(child);
            }
        }

        public ObservableCollection<BlockComponent> Childred { get; } = new ObservableCollection<BlockComponent>();
    }
}