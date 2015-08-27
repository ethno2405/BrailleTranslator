using System;
using BrailleTranslator.Desktop.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Model
{
    public class Component : ObservableObject
    {
        private string _title;

        private bool _isSelected;

        private bool _isExpanded = true;

        public Component()
        {
            _title = GetType().Name.ToLower();
        }

        public Component(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            _title = title;
        }

        public virtual bool IsVisible { get; } = true;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                Set(nameof(IsSelected), ref _isSelected, value);
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                Set(nameof(IsExpanded), ref _isExpanded, value);
            }
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