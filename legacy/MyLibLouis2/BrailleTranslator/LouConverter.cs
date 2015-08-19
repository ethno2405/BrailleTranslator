using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace MyLibLouis2.BrailleTranslator
{
    [ValueConversion(typeof(string), typeof(string))]
    public class LouConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = (string)value;
            return LouTranslate(strValue);
        }

        private object LouTranslate(string strValue)
        {
            string tableList = "ru-litbrl.ctb,hyph_ru.dic";
            StringBuilder inbuf = new StringBuilder(strValue);
            int inlen = inbuf.Length;
            StringBuilder outbuf = new StringBuilder(inlen * 2);
            int outlen = outbuf.Capacity;

            int result;
            result = BrailleTranslator.LibLouis.lou_translateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);
            string str = outbuf.ToString(0, outlen);
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = (string)value;
            return LouBackTranslate(strValue);
        }


        private string LouBackTranslate(string strValue)
        {
            string tableList = "ru-litbrl.ctb,hyph_ru.dic";
            StringBuilder inbuf = new StringBuilder(strValue);
            int inlen = inbuf.Length;
            StringBuilder outbuf = new StringBuilder(inlen * 2);
            int outlen = outbuf.Capacity;

            int result;
            result = LibLouis.lou_backTranslateString(tableList, inbuf, ref inlen, outbuf, ref outlen, null, null, 0);
            string str = outbuf.ToString(0, outlen);
            return str;
        }


    } // end class converter
    }
