using System.Windows;
using System.Windows.Controls;

namespace BGAssist.Braille.Client.Logic
{
    public enum ContentBoxType
    {
        Header,
        PageContent,
        Footer
    }

    public enum SourceType
    {
        Text,
        Heading,
        Note,
        StaticText,
        Macros
    }

    public abstract class Box : UserControl
    {
        private int cells;
        private int rows;

        public Box(int rows, int cells)
        {
            Rows = rows;
            Cells = cells;
        }

        public int Cells
        {
            get
            {
                return cells;
            }
            set
            {
                if (value <= 0)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    cells = value;
                }
            }
        }

        public string Description { get; set; }

        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                if (value <= 0)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    rows = value;
                }
            }
        }
    }
}