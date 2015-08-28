using System.Windows.Input;

namespace BrailleTranslator.Desktop.Messages
{
    public class KeyShortcutMessageToken
    {
        private KeyShortcutMessageToken(Key key, ModifierKeys modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public Key Key { get; }

        public ModifierKeys Modifiers { get; }

        public static KeyShortcutMessageToken Create(Key key, ModifierKeys modifiers)
        {
            return new KeyShortcutMessageToken(key, modifiers);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var token = obj as KeyShortcutMessageToken;

            if (token == null) return false;

            return Key == token.Key && Modifiers == token.Modifiers;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() ^ Modifiers.GetHashCode();
        }
    }
}