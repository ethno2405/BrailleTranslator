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
    }
}