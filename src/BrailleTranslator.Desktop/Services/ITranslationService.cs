namespace BrailleTranslator.Desktop.Services
{
    public interface ITranslationService
    {
        string TranslateToBraille(string input);

        string TranslateFromBraille(string input);
    }
}