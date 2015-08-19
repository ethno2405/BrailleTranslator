using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using MyLibLouis2.BrailleTranslator;


namespace MyLibLouis2
{
    public class Program
    {


        static string tableList = "./tables/en-us-g1.ctb,hyph_en_US.dic";
        static StringBuilder inbuf = new StringBuilder(256);
        static StringBuilder outbuf = new StringBuilder(256);
        static int inlen = 255;
        static int outlen = 255;

        public static int resultMain;


        static void Main(string[] args)
        {
            Translate();
        }

        public static void Translate()
        {
            int result = LibLouis.lou_translateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);
        }
    }
}
