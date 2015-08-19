using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace BGAssist.Braille.Client.Logic.Translation
{
    public class Translator
    {
        const string tableList = "./Resources/tables/en-us-g1.ctb,hyph_en_US.dic"; //bg-litbrl.ctb

        public static FlowDocument Translate(FlowDocument input, bool backTranslate = false)
        {
            try
            {
                int inlen = Int16.MaxValue;
                int outlen = Int16.MaxValue;
                StringBuilder inbuf = new StringBuilder(Int16.MaxValue);
                StringBuilder outbuf = new StringBuilder(Int16.MaxValue);

                inbuf.Clear();
                
                inbuf.Append(PageManager.FlowDocumentGetText(input));

                int result;
                if (backTranslate)
                {
                    result = LibLouis.lou_backTranslateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);
                }
                else
                {
                    result = LibLouis.lou_translateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);
                }

                if (result < 1)
                {
                    MessageBox.Show("Error translating!", "Error");
                }

                outbuf = RemoveUnsupportedCharacters(outbuf.ToString());

                return PageManager.FlowDocumentSetText(outbuf.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
                return input;
            }
        }

        private static StringBuilder RemoveUnsupportedCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (char c in str)
                {
                    if ((c >= ' ' && c <= '~'))
                    {
                        sb.Append(c);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
            return sb;
        }
    }
}