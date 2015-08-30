using System;
using System.Windows.Documents;

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
        }

        public FlowDocumentWrapper(string title) : base(title)
        {
            DocumentRoot = this;

            _document = _defaultDocument;

            PopulateChildren(_document.Blocks);
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

        protected override void PopulateChildren(TextElement textElement)
        {
            throw new NotSupportedException();
        }
    }
}