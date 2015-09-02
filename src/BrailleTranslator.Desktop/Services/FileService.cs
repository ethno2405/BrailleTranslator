using System;
using System.IO;

namespace BrailleTranslator.Desktop.Services
{
    public class FileService : IFileService, IDisposable
    {
        private FileStream _fileStream;

        public FileService(FileStream fileStream)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            _fileStream = fileStream;
        }

        public FileService()
        {
        }

        ~FileService()
        {
            Dispose(false);
        }

        protected bool IsDisposed { get; set; }

        public string Open(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void Save(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;

            _fileStream.Dispose();
            _fileStream = null;
            GC.SuppressFinalize(this);
        }
    }
}