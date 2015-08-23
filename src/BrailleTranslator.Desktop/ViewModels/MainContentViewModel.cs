using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.ViewModels
{
    public class MainContentViewModel : ViewModelBase
    {
        private string _text;

        public MainContentViewModel()
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut urna nulla, suscipit ut est mollis, volutpat imperdiet felis. Donec volutpat bibendum magna id euismod. Morbi tellus erat, tincidunt euismod malesuada ac, aliquam mollis justo. Nullam cursus in eros non posuere. Duis sit amet cursus nulla. Nam quis malesuada sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam porttitor sodales diam.";
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                Set(nameof(Text), ref _text, value);
            }
        }
    }
}