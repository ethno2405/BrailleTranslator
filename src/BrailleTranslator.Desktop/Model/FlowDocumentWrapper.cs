using System;
using System.Windows.Documents;
using System.Windows.Input;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.Model
{
    public class FlowDocumentWrapper : Component
    {
        private static readonly FlowDocument _defaultDocument = new FlowDocument(new Volume(new Section(new Paragraph(new Run(string.Empty)))));

        private FlowDocument _document;

        private TextPointer _caretPosition;

        public FlowDocumentWrapper()
        {
            DocumentRoot = this;
            SubscribeForMessages();
        }

        public FlowDocumentWrapper(string title) : base(title)
        {
            DocumentRoot = this;

            _document = _defaultDocument;

            PopulateChildren(_document.Blocks);
            SubscribeForMessages();
        }

        public FlowDocument Document
        {
            get
            {
                return _document;
            }
            set
            {
                _document = value;

                PopulateChildren(_document.Blocks);

                RaisePropertyChanged(nameof(Document));
                RaisePropertyChanged(nameof(PlainText));
            }
        }

        public string PlainText
        {
            get
            {
                return new TextRange(_document.ContentStart, _document.ContentEnd).Text;
            }
        }

        public TextPointer CaretPosition
        {
            get
            {
                return _caretPosition;
            }
            set
            {
                Set(nameof(CaretPosition), ref _caretPosition, value);
            }
        }

        public override string CreateChildText
        {
            get
            {
                return "New volume";
            }
        }

        protected override TextElement Payload
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        protected override bool CanDelete()
        {
            return false;
        }

        protected override void RemoveChild(Component component)
        {
            var blockComponent = component as BlockComponent;

            if (blockComponent == null) throw new InvalidOperationException(string.Format("Component is of type {0}, but {1} is expected.", component.GetType(), typeof(BlockComponent)));

            Document.Blocks.Remove(blockComponent.Block);

            Children.Remove(component);
        }

        protected override bool CanMoveUp()
        {
            return false;
        }

        protected override bool CanMoveDown()
        {
            return false;
        }

        protected override void PopulateChildren(TextElement textElement)
        {
            throw new NotSupportedException();
        }

        protected override TextElement CreateChildElement()
        {
            var volume = new Volume(new Section(new Paragraph(new Run(string.Empty))));
            Document.Blocks.Add(volume);

            return volume;
        }

        private void SubscribeForMessages()
        {
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Enter, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt), m => CreateChildCommand.Execute(null));
        }
    }
}