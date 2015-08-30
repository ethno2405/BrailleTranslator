using System;
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
            RegisterCommands();
            SubscribeForMessages();
        }

        public Component(string title) : base(title)
        {
            RegisterCommands();
            SubscribeForMessages();
        }

        public static FlowDocumentWrapper DocumentRoot { get; set; }

        public ICommand DeleteComponentCommand { get; private set; }

        public ICommand MoveUpCommand { get; private set; }

        public ICommand MoveDownCommand { get; private set; }

        public ICommand CreateChildCommand { get; private set; }

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

        public virtual string CreateChildText { get; } = "New component";

        public virtual bool CanCreateChildComponent
        {
            get
            {
                return true;
            }
        }

        protected static bool IsMoving { get; set; }

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
            if (!CanDelete()) return;

            Parent?.RemoveChild(this);
        }

        protected bool IsLast()
        {
            var parent = Parent;
            while (parent.Parent != null && parent.Children.Count > 1)
            {
                parent = parent.Parent;
            }

            var last = parent.Children.LastOrDefault();

            while (last != null && last != this)
            {
                last = last.Children.LastOrDefault();
            }

            return this == last;
        }

        protected abstract void RemoveChild(Component component);

        protected abstract void PopulateChildren(TextElement textElement);

        protected void PopulateChildren(IEnumerable<TextElement> textElements)
        {
            if (IsMoving) return;

            if (textElements.Count() < Children.Count)
            {
                var childrenToRemove = Children.Where(x => !textElements.Contains(x.Payload));

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
            return IsSelected && Parent.Children.Count > 1;
        }

        protected virtual bool CanMoveUp()
        {
            return IsSelected && Parent.Children.First() != this;
        }

        protected virtual bool CanMoveDown()
        {
            return IsSelected && Parent.Children.Last() != this;
        }

        protected virtual void MoveUp()
        {
            if (!CanMoveUp()) return;

            var currentIndex = Parent.Children.IndexOf(this);

            Parent.Children.Move(currentIndex, currentIndex - 1);
        }

        protected virtual void MoveDown()
        {
            if (!CanMoveDown()) return;

            var currentIndex = Parent.Children.IndexOf(this);

            Parent.Children.Move(currentIndex, currentIndex + 1);
        }

        protected abstract TextElement CreateChildElement();

        private void CreateChild()
        {
            var element = CreateChildElement();

            if (element == null) throw new InvalidOperationException("CreateChildElement() returned null.");

            Messenger.Default.Send(new NotificationMessageAction<string>(this, CreateChildText, t =>
            {
                Children.First(x => x.Payload == element).Title = t;
            }), Tokens.NewComponent);
        }

        private void RegisterCommands()
        {
            DeleteComponentCommand = new RelayCommand(Delete, CanDelete);
            MoveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            MoveDownCommand = new RelayCommand(MoveDown, CanMoveDown);
            CreateChildCommand = new RelayCommand(CreateChild, () => CanCreateChildComponent);
        }

        private void SubscribeForMessages()
        {
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Delete, ModifierKeys.None), m => Delete());
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Up, ModifierKeys.Control), m => MoveUp());
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Down, ModifierKeys.Control), m => MoveDown());
        }
    }
}