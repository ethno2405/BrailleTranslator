using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BGAssist.Braille.Client.Logic.DocumentStructure
{
    public class Document : Block
    {
        private string _title;
        ObservableCollection<Volume> _VolumeCollection = new ObservableCollection<Volume>();


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public Document(string name)
        {
            Title = name;
        }

        public Document()
        {
            // TODO: Complete member initialization
        }
        public ObservableCollection<Volume> VolumeCollection
        { get { return _VolumeCollection; } }
    }
}
