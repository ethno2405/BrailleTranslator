using System;

namespace BrailleTranslator.Desktop.Model
{
    public class VolumeComponent : BlockComponent
    {
        public VolumeComponent()
        {
        }

        public VolumeComponent(string title) : base(title)
        {
        }

        public VolumeComponent(string title, Volume volume) : base(title, volume)
        {
            PopulateChildren(volume.Blocks);
        }

        public VolumeComponent(Volume volume) : base(volume)
        {
            PopulateChildren(volume.Blocks);
        }

        protected override void RemoveChild(Component component)
        {
            base.RemoveChild(component);

            var blockComponent = component as BlockComponent;

            if (blockComponent == null) throw new InvalidOperationException(string.Format("Component is of type {0}, but type {1} is expected.", component.GetType(), typeof(BlockComponent)));

            (Block as Volume).Blocks.Remove(blockComponent.Block);

            Children.Remove(component);
        }
    }
}