using System;

namespace BrailleTranslator.Desktop.Services
{
    public interface IWindowService
    {
        void Open(object dataContext, Action callback);

        void Close(object dataContext);
    }
}