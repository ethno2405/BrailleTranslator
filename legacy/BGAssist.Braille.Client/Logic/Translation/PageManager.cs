using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BGAssist.Braille.Client.Logic.Translation
{
    class PageManager
    {
        public static string FlowDocumentGetText(FlowDocument fd)
        {
            TextRange tr = new TextRange(fd.ContentStart, fd.ContentEnd);
            return tr.Text;
        }

        public static FlowDocument FlowDocumentSetText(String text)
        {
            Paragraph p = new Paragraph();
            p.Inlines.Add(text);

            FlowDocument fd = new FlowDocument();

            fd.Blocks.Add(p);
            return fd;
        }
    }
}
