using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BGAssist.Braille.Client.Logic.DocumentStructure
{
    public class Paragraphe : Block
    {
        private string _title;
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public Paragraphe(string name, string text)
        {
            Title = name;
            Text = text;
        }
    }
}
