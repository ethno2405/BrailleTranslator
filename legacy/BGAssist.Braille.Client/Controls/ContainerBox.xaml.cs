using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BGAssist.Braille.Client.Logic;

namespace BGAssist.Braille.Client.Controls
{
    public partial class ContainerBox : Box
    {
        private List<ContentBox> contentBoxes;
        
        public List<ContentBox> ContentBoxes 
        {
            get
            {
                return contentBoxes;
            }
        }

        public ContainerBox(int rows, int cells)
            :base(rows, cells)
        {
            if (ContentBoxes == null)
            {
                contentBoxes = new List<ContentBox>();
            }
        }

        public void InsertContenBox(ContentBox box)
        {
            if (box.Rows > this.Rows)
            {
                MessageBox.Show("You are creating a content box with more rows than the page! The maximum rows count is " + this.Rows, "Error", MessageBoxButton.OK);
            }
            else if (box.Cells > this.Cells)
            {
                MessageBox.Show("You are creating a content box with more cells than the page! The maximum cells count is " + this.Cells, "Error", MessageBoxButton.OK);
            }
            else
            {
                contentBoxes.Add(box);
            }
        }

        public static ContainerBox CreateDefaultPage()
        {
            int contentWidth = Properties.Settings.Default.PageCells - (Properties.Settings.Default.PagePadding * 2);
            int footerHeight = Properties.Settings.Default.PageRows - (Properties.Settings.Default.HeaderHeight + Properties.Settings.Default.ContentHeight);

            ContainerBox cb = new ContainerBox(Properties.Settings.Default.PageRows, Properties.Settings.Default.PageCells);

            ContentBox header = new ContentBox(Properties.Settings.Default.HeaderHeight, contentWidth);
            header.Type = ContentBoxType.Header;

            ContentBox pageContent = new ContentBox(Properties.Settings.Default.ContentHeight, contentWidth);
            pageContent.Type = ContentBoxType.PageContent;

            ContentBox footer = new ContentBox(footerHeight, contentWidth);
            footer.Type = ContentBoxType.Footer;

            cb.InsertContenBox(header);
            cb.InsertContenBox(pageContent);
            cb.InsertContenBox(footer);

            return cb;
        }
    }
}
