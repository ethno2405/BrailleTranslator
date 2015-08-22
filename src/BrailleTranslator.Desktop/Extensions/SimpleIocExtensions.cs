using System;

namespace GalaSoft.MvvmLight.Ioc
{
    public static class SimpleIocExtensions
    {
        public static SimpleIoc Register<T>(this SimpleIoc container, Func<SimpleIoc, T> factory) where T : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            T instance = factory(container);

            if (instance == null) throw new InvalidOperationException(string.Concat("Cannot create instance of type ", typeof(T)));

            container.Register(() => { return instance; });

            return container;
        }
    }
}