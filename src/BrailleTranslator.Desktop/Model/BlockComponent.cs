using System;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class BlockComponent : Component
    {
        public BlockComponent()
        {
        }

        public BlockComponent(string title) : base(title)
        {
        }

        public BlockComponent(string title, Block block) : base(title)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Block = block;
        }

        public BlockComponent(Block block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Block = block;
        }

        public Block Block { get; set; }

        protected override void RemoveChild(Component component)
        {
            if (Block.SiblingBlocks.Count == 0) return;
        }
    }
}