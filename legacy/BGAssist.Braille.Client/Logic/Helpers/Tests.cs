using BGAssist.Braille.Client.Logic.Translation;
using BGAssist.Braille.Client.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace BGAssist.Braille.Client.Logic.Helpers
{
    /// <summary>
    ///
    /// </summary>
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TranslateWithLibLouis()
        {
            StringBuilder inbuf = new StringBuilder(Settings.Default.BufferSize);
            StringBuilder outbuf = new StringBuilder(Settings.Default.BufferSize);
            int inlen = Settings.Default.BufferLength;
            int outlen = Settings.Default.BufferLength;

            string str = "2.5.4"; //LibLouis.lou_version();
            Console.WriteLine("Yes, the lou_version() returned {0}", str);

            inbuf.Append("abcasdf 123");
            Console.WriteLine("inbuf is {0}", inbuf.ToString());
            Console.WriteLine("outbuf is {0}", outbuf.ToString());
            Console.WriteLine("inlen is {0}", inlen.ToString());
            Console.WriteLine("outlen is {0}", outlen.ToString());
            Console.WriteLine("tableList is {0}", Settings.Default.BrailleTable);

            int result;
            result = LibLouis.lou_translateString(Settings.Default.BrailleTable, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);

            Console.WriteLine("result is {0} ", result);
            Console.WriteLine("inbuf is {0}", inbuf.ToString());
            Console.WriteLine("outbuf is {0}", outbuf.ToString());
            Console.WriteLine("inlen is {0}", inlen.ToString());
            Console.WriteLine("outlen is {0}", outlen.ToString());
            Console.WriteLine("tableList is {0}", Settings.Default.BrailleTable);

            Console.WriteLine("hello from myLibLouis2!");
            Console.ReadLine();
        }
    }
}