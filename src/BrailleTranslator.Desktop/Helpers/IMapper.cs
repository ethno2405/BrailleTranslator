using System;
using System.Collections.Generic;
using System.Windows;

namespace BrailleTranslator.Desktop.Helpers
{
    public interface IMapper
    {
        IMapper Map(Type viewModelType, Type viewType);

        IMapper Map<TViewModel, TView>() where TView : FrameworkElement;

        Type Resolve<TViewModel>();

        Type Resolve(Type viewModelType);

        IDictionary<Type, Type> GetMappings();
    }
}