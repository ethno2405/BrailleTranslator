using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Books
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            XmlDataProvider dataProvider = this.FindResource("xmlDataProvider") as XmlDataProvider;
            XmlDocument doc = new XmlDocument();
            doc.Load("braile.xml");
            dataProvider.Document = doc;
            dataProvider.XPath = "*";
        }

        private void bookTree_Selected(object sender, RoutedEventArgs e)
        {
            XmlElement element = bookTree.SelectedItem as XmlElement;
            content.Text = element.InnerText;
        }
    }

    public class XmlNodeFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            XmlNode node = value as XmlNode;
            if (node != null && targetType == typeof(string))
            {
                if ((node.Name == "Volume") || (node.Name == "Section"))
                {
                    return node.Attributes["Title"].Value;
                }
                /*else if (node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == XmlNodeType.Text)
                    return node.Name + " : " + node.InnerText;*/
                else
                    return node.Name;
            }
            else
                return "";
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}