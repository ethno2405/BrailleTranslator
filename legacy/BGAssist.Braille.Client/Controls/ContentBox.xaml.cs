using BGAssist.Braille.Client.Logic;

namespace BGAssist.Braille.Client.Controls
{
    /// <summary>
    /// Interaction logic for ContentBox.xaml
    /// </summary>
    public partial class ContentBox : Box
    {
        public ContentBox(int rows, int cells)
            : base(rows, cells)
        {
        }

        public int Down { get; set; }

        public int Left { get; set; }

        public int MinimumHeight { get; set; }

        public int MinimumWidth { get; set; }

        public int Right { get; set; }

        public SourceType Source { get; set; }

        public ContentBoxType Type { get; set; }

        public int Up { get; set; }
    }
}