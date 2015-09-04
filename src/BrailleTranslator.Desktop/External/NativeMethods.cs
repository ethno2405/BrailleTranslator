using System.Runtime.InteropServices;
using System.Text;

namespace BrailleTranslator.Desktop.External
{
    internal class NativeMethods
    {
        private const string liblouisDllFilePath = "/External/liblouis-2.dll";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0")]
        [DllImport(liblouisDllFilePath)]
        internal static extern int lou_translateString(
            string tableList,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
            ref int inlen,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
            ref int outlen,
            string typeform,
            string spacing,
            int mode);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0")]
        [DllImport(liblouisDllFilePath)]
        internal static extern int lou_backTranslateString(
            string tableList,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder inbuf,
            ref int inlen,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder outbuf,
            ref int outlen,
            string typeform,
            string spacing,
            int mode);
    }
}