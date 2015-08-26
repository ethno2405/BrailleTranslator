using System;
using System.Windows.Documents;
using BrailleTranslator.Desktop.Model;

namespace BrailleTranslator.Desktop.Helpers
{
    public class ComponentFactory : IComponentFactory
    {
        public BlockComponent CreateBlockComponent(Block block)
        {
            if (block is Paragraph)
            {
                return new ParagraphComponent("paragraph", block as Paragraph);
            }

            if (block is Section)
            {
                return new SectionComponent("section", block as Section);
            }

            throw new NotSupportedException(string.Format("Not supported type {0}.", block.GetType()));
        }

        public InlineComponent CreateInlineCollection(Inline inline)
        {
            if (inline is Run)
            {
                return new RunComponent("run", inline as Run);
            }

            throw new NotSupportedException(string.Format("Not supported type {0}.", inline.GetType()));
        }
    }
}