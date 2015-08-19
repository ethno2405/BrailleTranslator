using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BGAssist.Braille.Client.Logic.DocumentStructure
{
    public class Volume : Block
    {
        public string Title { get; set; }
        private ObservableCollection<System.Windows.Documents.Section> childSectionsValue = new ObservableCollection<System.Windows.Documents.Section>();
        public ObservableCollection<System.Windows.Documents.Section> ChildSections
        {
            get
            {
                return childSectionsValue;
            }
            set
            {
                childSectionsValue = value;
            }
        }
        
        public Volume(string title)
        {
            this.Title = title;
        }/**/


        public Volume()
        {
            // TODO: Complete member initialization
        }




    }
}
