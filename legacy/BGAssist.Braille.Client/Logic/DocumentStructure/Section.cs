using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BGAssist.Braille.Client.Logic.DocumentStructure
{
    public class Section : Block
    {
        public string Title { get; set; }
        private ObservableCollection<Paragraph> childParagraphsValue = new ObservableCollection<Paragraph>();
        public ObservableCollection<Paragraph> ChildParagraphs
        {
            get
            {
                return childParagraphsValue;
            }
            set
            {
                childParagraphsValue = value;
            }
        }

        public Section(string title)
        {
            this.Title = title;
        }/**/


        public Section()
        {
            // TODO: Complete member initialization
        }
    }
}
