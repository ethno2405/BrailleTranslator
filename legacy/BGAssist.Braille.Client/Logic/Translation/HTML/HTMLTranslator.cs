﻿using System;
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
    public class HTMLTranslator : IValueConverter
    {
        private static XslCompiledTransform ToHtmlTransform;

        private static XslCompiledTransform ToXamlTransform;

        public HTMLTranslator()
        {
            if (ToHtmlTransform == null)
            {
                ToHtmlTransform = LoadTransformResource("Logic/Translation/HTML/FlowDocumentHTML.xslt");
            }
            if (ToXamlTransform == null)
            {
                ToXamlTransform = LoadTransformResource("Logic/Translation/HTML/HTMLFlowDocument.xslt");
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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new FlowDocument();
            }
            if (value is FlowDocument)
            {
                return value;
            }
            if (targetType != typeof(FlowDocument))
            {
                throw new InvalidOperationException(
                    "FlowDocumentToHtmlConverter can only convert to a FlowDocument.");
            }
            if (!(value is string))
            {
                throw new InvalidOperationException(
                    "FlowDocumentToHtmlConverter can only convert from a string or FlowDocument.");
            }

            string s = (string)value;

            FlowDocument d;

            using (MemoryStream ms = new MemoryStream())
            using (StringReader sr = new StringReader(s))
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;
                using (XmlReader xr = XmlReader.Create(sr))
                using (XmlWriter xw = XmlWriter.Create(ms, xws))
                {
                    ToXamlTransform.Transform(xr, xw);
                }
                ms.Seek(0, SeekOrigin.Begin);

                d = XamlReader.Load(ms) as FlowDocument;
            }
            XamlWriter.Save(d, Console.Out);
            return d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is FlowDocument))
            {
                return null;
            }
            if (targetType == typeof(FlowDocument))
            {
                return value;
            }

            if (targetType != typeof(string))
            {
                throw new InvalidOperationException(
                    "FlowDocumentToHtmlConverter can only convert back from a FlowDocument to a string.");
            }

            FlowDocument d = (FlowDocument)value;

            using (MemoryStream ms = new MemoryStream())
            {
                // write XAML out to a MemoryStream
                TextRange tr = new TextRange(
                    d.ContentStart,
                    d.ContentEnd);
                tr.Save(ms, DataFormats.Xaml);
                ms.Seek(0, SeekOrigin.Begin);

                // transform the contents of the MemoryStream to HTML
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    XmlWriterSettings xws = new XmlWriterSettings();
                    xws.OmitXmlDeclaration = true;
                    XmlReader xr = XmlReader.Create(ms);
                    XmlWriter xw = XmlWriter.Create(sw, xws);
                    ToHtmlTransform.Transform(xr, xw);
                }
                return sb.ToString();
            }
        }

        #endregion IValueConverter Members
    }
}