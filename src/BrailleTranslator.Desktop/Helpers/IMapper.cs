using System;
using System.Windows;

namespace BrailleTranslator.Desktop.Helpers
{
    public interface IMapper
    {
        IMapper Map(Type viewModelType, Type viewType);

        IMapper Map<TViewModel, TView>() where TView : FrameworkElement;
    }
}