using System;
using BrailleTranslator.Desktop.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class Component : ObservableObject
    {
        private string _title;

        private bool _isSelected;

        private bool _isExpanded = true;

        private ObservableObject _parent;

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

        public ObservableObject Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                Set(nameof(Parent), ref _parent, value);
            }
        }

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