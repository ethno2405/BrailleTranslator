using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using BrailleTranslator.Desktop.Helpers;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class Component : VisualNode
    {
        private Component _parent;

        public Component()
        {
            DeleteComponentCommand = new RelayCommand(Delete, () => CanDelete());

            SubscribeForMessages();
        }

        public Component(string title) : base(title)
        {
            DeleteComponentCommand = new RelayCommand(Delete, () => CanDelete());

            SubscribeForMessages();
        }

        public static FlowDocumentWrapper DocumentRoot { get; set; }

        public ICommand DeleteComponentCommand { get; }

        public virtual ObservableCollection<Component> Children { get; private set; } = new ObservableCollection<Component>();

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

        protected abstract TextElement Payload { get; set; }

        public virtual void Delete()
        {
            if (CanDelete())
            {
                Parent?.RemoveChild(this);
            }
        }

        protected abstract void RemoveChild(Component component);

        protected abstract void PopulateChildren(TextElement textElement);

        protected void PopulateChildren(IEnumerable<TextElement> textElements)
        {
            if (textElements.Count() < Children.Count)
            {
                var childrenToRemove = Children.Where(x => !textElements.Contains(x.Payload));

                if (childrenToRemove.Any(x => x.IsSelected))
                {
                }

                Children.RemoveRange(childrenToRemove);
            }

            foreach (var element in textElements)
            {
                var child = Children.FirstOrDefault(x => x.Payload != null && x.Payload == element);

                if (child == null)
                {
                    child = ComponentFactory.CreateComponent(element);

                    child.Parent = this;
                    Children.Add(child);
                }
                else
                {
                    child.PopulateChildren(element);
                }
            }
        }

        protected virtual bool CanDelete()
        {
            return Parent?.Children.Count > 1;
        }

        private void SubscribeForMessages()
        {
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Delete, ModifierKeys.None),
                (m) =>
                {
                    if (IsSelected)
                    {
                        Delete();
                    }
                });
        }
    }
}