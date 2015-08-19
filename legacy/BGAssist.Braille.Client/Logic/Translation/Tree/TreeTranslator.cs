using BGAssist.Braille.Client.Logic.DocumentStructure;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Xsl;

namespace BGAssist.Braille.Client.Logic.Translation
{
    public class TreeTranslator : IValueConverter
    {
        private static XslCompiledTransform ToTreeTransform;

        private static XslCompiledTransform ToXamlTransform;

        public TreeTranslator()
        {
            if (ToTreeTransform == null)
            {
                ToTreeTransform = LoadTransformResource("/Logic/Translation/Tree/FlowDocumentTree.xslt");
            }
            if (ToXamlTransform == null)
            {
                ToXamlTransform = LoadTransformResource("/Logic/Translation/Tree/TreeFlowDocument.xslt");
            }
        }

        private static XslCompiledTransform LoadTransformResource(string path)
        {
            Uri uri = new Uri(path, UriKind.Relative);
            XmlReader xr = XmlReader.Create(Application.GetResourceStream(uri).Stream);
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xr);
            return xslt;
        }

        #region IValueConverter Members

        // from xml to flowdocument
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is FlowDocument))
            {
                return null;
            }
            if (targetType == typeof(FlowDocument))
            {
                return value;
            }

            if (targetType != typeof(XmlDocument))
            {
                throw new InvalidOperationException("TreeFlowDocumentConverter can only convert back from a FlowDocument to a string.");
            }

            FlowDocument document = (FlowDocument)value;
            XmlManipulator xmlManipulator = new XmlManipulator();
            XmlDocument sourceXML = xmlManipulator.GetXML(document);
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(sourceXML.OuterXml));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                XmlWriter xw = XmlWriter.Create(sw);
                ToTreeTransform.Transform(xmlReader, xw);
            }

            XmlDocument result = new XmlDocument();
            result.LoadXml(sb.ToString());
            return result;
        }

        // from flowdocument to xml
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO: Implement back convertion from XMLDocument to FlowDocument
            return new object();
        }

        #endregion IValueConverter Members
    }
}