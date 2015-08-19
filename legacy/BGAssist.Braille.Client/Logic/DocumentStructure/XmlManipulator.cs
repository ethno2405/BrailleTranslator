using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace BGAssist.Braille.Client.Logic.DocumentStructure
{
    public class XmlManipulator
    {
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public XmlDocument GetXML(FlowDocument flowDocument)
        {
            XmlDocument xmlDocument;
            string documentContent = XamlWriter.Save(flowDocument);
            xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(documentContent);
            return xmlDocument;
        }
    }
}
