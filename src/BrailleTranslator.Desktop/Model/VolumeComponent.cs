using System.Collections.ObjectModel;

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
            foreach (var section in volume.Sections)
            {
                var child = ComponentFactory.CreateBlockComponent(section);

                child.Parent = this;
                Children.Add(child as SectionComponent);
            }
        }

        public ObservableCollection<SectionComponent> Children { get; } = new ObservableCollection<SectionComponent>();
    }
}