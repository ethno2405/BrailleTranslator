using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class Volume : Section
    {
        public Volume()
        {
        }

        public Volume(Block block) : base(block)
        {
        }
    }
}