namespace BrailleTranslator.Desktop.Services
{
    public interface IFileService
    {
        string Open(string filePath);

        void Save(string content, string filePath);
    }
}