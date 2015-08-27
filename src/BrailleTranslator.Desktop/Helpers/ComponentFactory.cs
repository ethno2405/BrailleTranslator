using System;
using System.Windows.Documents;
using BrailleTranslator.Desktop.Model;

namespace BrailleTranslator.Desktop.Helpers
{
    public class ComponentFactory : IComponentFactory
    {
        public Component CreateComponent(TextElement textElement)
        {
            var elementType = textElement.GetType();
            var title = elementType.Name;

            if (elementType == typeof(Paragraph))
            {
                return new ParagraphComponent(title, textElement as Paragraph);
            }

            if (elementType == typeof(Volume))
            {
                return new VolumeComponent(title, textElement as Volume);
            }

            if (elementType == typeof(Section))
            {
                return new SectionComponent(title, textElement as Section);
            }

            if (elementType == typeof(Run))
            {
                return new RunComponent(title, textElement as Run);
            }

            throw new NotSupportedException(string.Format("Not supported type {0}.", elementType));
        }
    }
}