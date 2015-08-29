using System;
using System.Windows.Documents;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class BlockComponent : Component
    {
        public BlockComponent()
        {
            SubscribeForEvents();
        }

        public BlockComponent(string title) : base(title)
        {
            SubscribeForEvents();
        }

        public BlockComponent(string title, Block block) : base(title)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Block = block;

            SubscribeForEvents();
        }

        public BlockComponent(Block block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Block = block;

            SubscribeForEvents();
        }

        public Block Block { get; set; }

        private void SubscribeForEvents()
        {
            this.ForProperty(nameof(IsSelected))
                .When(() => IsSelected)
                .Subscribe(MoveCaretToComponentLocation);
        }

        private void MoveCaretToComponentLocation()
        {
        }
    }
}