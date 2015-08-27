using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class Volume : Block
    {
        public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();
    }
}