using System.IO;

namespace BrailleTranslator.Desktop.Services
{
    public class FileService : IFileService
    {
        public string Open(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void Save(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}