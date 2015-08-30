using System;
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

        protected override void RemoveChild(Component component)
        {
            var blockComponent = component as BlockComponent;

            if (blockComponent == null) throw new InvalidOperationException(string.Format("Component is of type {0}, but type {1} is expected.", component.GetType(), typeof(BlockComponent)));

            (Block as Section).Blocks.Remove(blockComponent.Block);

            Children.Remove(component);
        }

        protected override void PopulateChildren(TextElement textElement)
        {
            var section = textElement as Section;

            if (section == null) throw new ArgumentException(string.Concat("Text element is not of type ", GetType().FullName), nameof(textElement));

            PopulateChildren(section.Blocks);
        }
    }
}