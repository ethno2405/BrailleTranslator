using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BGAssist.Braille.Client
{
    public static class FormatCommand
    {
        
        static RoutedUICommand shortCut_AddVolume = new RoutedUICommand("Add volume with shortcut", "ShortCut_AddVolume", typeof(FormatCommand));
        public static RoutedUICommand ShortCut_AddVolume { get { return shortCut_AddVolume;}}

        static RoutedUICommand shortCut_AddSection = new RoutedUICommand("Add section with shortcut", "ShortCut_AddSection", typeof(FormatCommand));
        public static RoutedUICommand ShortCut_AddSection { get { return shortCut_AddSection; } }

        static RoutedUICommand shortCut_AddParagraph = new RoutedUICommand("Add paragraph with shortcut", "ShortCut_AddParagraph", typeof(FormatCommand));
        public static RoutedUICommand ShortCut_AddParagraph { get { return shortCut_AddParagraph; } }

        
    }
}
