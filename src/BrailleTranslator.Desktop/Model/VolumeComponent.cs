using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

                Children.Add(child as SectionComponent);
            }
        }

        public ObservableCollection<SectionComponent> Children { get; } = new ObservableCollection<SectionComponent>();
    }
}