using System.Text;
using BrailleTranslator.Desktop.External;

namespace BrailleTranslator.Desktop.Services
{
    public class TranslationService : ITranslationService
    {
        private const string tableList = "External/tables/en-us-g1.ctb,hyph_en_US.dic";

        public string TranslateToBraille(string input)
        {
            int inlen = short.MaxValue;
            int outlen = short.MaxValue;
            var inbuf = new StringBuilder(short.MaxValue);
            var outbuf = new StringBuilder(short.MaxValue);

            inbuf.Clear();

            inbuf.Append(input);

            var result = NativeMethods.lou_translateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);

            if (result != 0)
            {
                return "Unable to translate!";
            }

            return RemoveUnsupportedCharacters(outbuf.ToString());
        }

        public string TranslateFromBraille(string input)
        {
            int inlen = short.MaxValue;
            int outlen = short.MaxValue;
            var inbuf = new StringBuilder(short.MaxValue);
            var outbuf = new StringBuilder(short.MaxValue);

            inbuf.Clear();

            inbuf.Append(input);

            var result = NativeMethods.lou_backTranslateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);

            if (result != 0)
            {
                return "Unable to translate!";
            }

            return RemoveUnsupportedCharacters(outbuf.ToString());
        }

        private string RemoveUnsupportedCharacters(string str)
        {
            var sb = new StringBuilder();

            foreach (char c in str)
            {
                if ((c >= ' ' && c <= '~'))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}