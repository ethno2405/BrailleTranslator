using System;
using System.Collections.Generic;
using System.Windows;

namespace BrailleTranslator.Desktop.Helpers
{
    public class Mapper : IMapper
    {
        public IDictionary<Type, Type> Mappings { get; } = new Dictionary<Type, Type>();

        public IMapper Map<TViewModel, TView>() where TView : FrameworkElement
        {
            return Map(typeof(TViewModel), typeof(TView));
        }

        public IMapper Map(Type viewModelType, Type viewType)
        {
            if (viewModelType == null) throw new ArgumentNullException(nameof(viewModelType));
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));
            if (Mappings.ContainsKey(viewModelType)) throw new InvalidOperationException(string.Format("Type {0} is already mapped with {1}", viewModelType.FullName, viewType.FullName));

            Mappings.Add(viewModelType, viewType);

            return this;
        }

        public Type Resolve<TViewModel>()
        {
            return Resolve(typeof(TViewModel));
        }

        public Type Resolve(Type viewModelType)
        {
            if (!Mappings.ContainsKey(viewModelType)) throw new InvalidOperationException(string.Format("Type {0} is not mapped", viewModelType.FullName));

            return Mappings[viewModelType];
        }

        public IDictionary<Type, Type> GetMappings()
        {
            return Mappings;
        }
    }
}