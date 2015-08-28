using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.Messages
{
    public class KeyShortcutMessage : MessageBase
    {
        public KeyShortcutMessage(Key key, ModifierKeys modifierKeys)
        {
            Key = key;
            ModifierKeys = modifierKeys;
        }

        public KeyShortcutMessage(Key key, ModifierKeys modifierKeys, object sender) : base(sender)
        {
            Key = key;
            ModifierKeys = modifierKeys;
        }

        public KeyShortcutMessage(Key key, ModifierKeys modifierKeys, object sender, object target) : base(sender, target)
        {
            Key = key;
            ModifierKeys = modifierKeys;
        }

        public Key Key { get; }

        public ModifierKeys ModifierKeys { get; }
    }
}