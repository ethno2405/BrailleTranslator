using System.Windows.Documents;
using BrailleTranslator.Desktop.Model;

namespace BrailleTranslator.Desktop.Helpers
{
    public interface IComponentFactory
    {
        Component CreateComponent(TextElement textElement);
    }
}