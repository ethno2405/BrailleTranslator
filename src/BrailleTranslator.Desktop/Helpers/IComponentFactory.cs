using System.Windows.Documents;
using BrailleTranslator.Desktop.Model;

namespace BrailleTranslator.Desktop.Helpers
{
    public interface IComponentFactory
    {
        BlockComponent CreateBlockComponent(Block block);

        InlineComponent CreateInlineCollection(Inline inline);
    }
}