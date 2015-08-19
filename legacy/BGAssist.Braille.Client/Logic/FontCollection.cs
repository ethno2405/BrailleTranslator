using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace BGAssist.Braille.Client.Logic
{
    public class FontCollection
    {
        private static List<FontCollection> _fontsCollection;

        public FontCollection()
        {
        }

        public FontCollection(FontFamily NormalFont, FontFamily BrailleFont)
        {
            this.NormalFontValue = NormalFont;
            this.BrailleFontKey = BrailleFont;
        }

        public static List<FontCollection> FontsCollection
        {
            get
            {
                if (_fontsCollection == null)
                {
                    _fontsCollection = new List<FontCollection>();
                    var fonts = Fonts.GetFontFamilies(new Uri("pack://application:,,,/Resources/"));
                    
                    for (int i = 0; i < fonts.Count; i++)
                    {
                        FontCollection font = new FontCollection();
                        font.BrailleFontKey = fonts.ElementAt(i);
                        font.NormalFontValue = Fonts.SystemFontFamilies.ElementAt(i);
                        _fontsCollection.Add(font);
                    }
                }

                return _fontsCollection;
            }
        }

        public FontFamily NormalFontValue { get; set; }

        public FontFamily BrailleFontKey { get; set; }
    }
}