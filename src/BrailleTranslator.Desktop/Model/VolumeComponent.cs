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

        public VolumeComponent(string title, Section block) : base(title, block)
        {
        }

        public ObservableCollection<SectionComponent> Children { get; } = new ObservableCollection<SectionComponent>();
    }
}