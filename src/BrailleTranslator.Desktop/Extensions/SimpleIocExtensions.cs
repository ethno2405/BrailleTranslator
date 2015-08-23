using System;

namespace GalaSoft.MvvmLight.Ioc
{
    public static class SimpleIocExtensions
    {
        public static SimpleIoc Register<T>(this SimpleIoc container, Func<SimpleIoc, T> factory) where T : class
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            container.Register(() => factory(container));

            return container;
        }
    }
}