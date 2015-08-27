using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using BrailleTranslator.Desktop.Helpers;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class Component : VisualNode
    {
        private Component _parent;

        public Component()
        {
            DeleteComponentCommand = new RelayCommand(Delete, CanDeleteComponent);
        }

        public Component(string title)
        {
            DeleteComponentCommand = new RelayCommand(Delete, CanDeleteComponent);
        }

        public ICommand DeleteComponentCommand { get; }

        public ObservableCollection<Component> Children { get; } = new ObservableCollection<Component>();

        public Component Parent
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

        protected IComponentFactory ComponentFactory
        {
            get
            {
                return SimpleIoc.Default.GetInstance<IComponentFactory>();
            }
        }

        public virtual void Delete()
        {
            Parent?.Children?.Remove(this);
        }

        protected void PopulateChildren(IEnumerable<TextElement> textElements)
        {
            foreach (var element in textElements)
            {
                var child = ComponentFactory.CreateComponent(element);

                child.Parent = this;
                Children.Add(child);
            }
        }

        protected virtual bool CanDeleteComponent()
        {
            return true;
        }
    }
}