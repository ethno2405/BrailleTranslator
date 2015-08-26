using System;
using BrailleTranslator.Desktop.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class Component : ObservableObject
    {
        private string _title;

        public Component()
        {
            _title = GetType().Name.ToLower();
        }

        public Component(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            _title = title;
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(nameof(Title), ref _title, value);
            }
        }

        protected IComponentFactory ComponentFactory
        {
            get
            {
                return SimpleIoc.Default.GetInstance<IComponentFactory>();
            }
        }
    }
}