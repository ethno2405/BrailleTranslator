using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MyLibLouis2.BrailleTranslator
{
    public class LibLouis
    {

        [DllImport("liblouis-2.dll")]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string lou_version();

        [DllImport("liblouis-2.dll")]
        public static extern int lou_translateString(
            string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        ref int inlen,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
        ref int outlen,
         string typeform,
            string spacing,
            int mode);


        [DllImport("liblouis-2.dll")]
        public static extern int lou_translate(
                string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        ref int inlen,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
        ref int outlen,
         string typeform,
            string spacing,
            ref int outputPos,
            ref int inputPos,
            ref int cursorPos,
            int mode);




        [DllImport("liblouis-2.dll")]
        public static extern int lou_backTranslateString(
            string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        ref int inlen,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
        ref int outlen,
         string typeform,
            string spacing,
            int mode);


        [DllImport("liblouis-2.dll")]
        public static extern int lou_backTranslate(
                string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        ref int inlen,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
        ref int outlen,
         string typeform,
            string spacing,
            ref int outputPos,
            ref int inputPos,
            ref int cursorPos,
            int mode);




        [DllImport("liblouis-2.dll")]
        public static extern int lou_hyphenate(
                string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        int inlen,
        [MarshalAs(UnmanagedType.LPStr)] StringBuilder outbuf,
            int mode);

        [DllImport("liblouis-2.dll")]
        public static extern int lou_compileString(
        string tableList,
        string inString);

        [DllImport("liblouis-2.dll")]
        public static extern int lou_dotsToChar(
        string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
        int length,
        int mode);


        [DllImport("liblouis-2.dll")]
        public static extern int lou_charToDots(
        string tableList,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
        int length,
        int mode);

        [DllImport("liblouis-2.dll")]
        public static extern void lou_logFile(
        string fileName);

        // Todo: the parameter should be reformated.
        [DllImport("liblouis-2.dll")]
        public static extern void lou_logPrint(string formatedString);

        [DllImport("liblouis-2.dll")]
        public static extern void lou_logEnd();


        [DllImport("liblouis-2.dll")]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string lou_setDataPath(
        string path);

        [DllImport("liblouis-2.dll")]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string lou_getDataPath();

        [DllImport("liblouis-2.dll")]
        public static extern IntPtr lou_getTable(string tableList);

        // Todo: The return should be char.
        [DllImport("liblouis-2.dll")]
        public static extern int lou_readCharFromFile(string fileName, ref int mode);


        [DllImport("liblouis-2.dll")]
        public static extern void lou_free();



    }
}
