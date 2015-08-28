using System;
using System.Windows.Documents;

namespace BrailleTranslator.Desktop.Model
{
    public class FlowDocumentWrapper : Component
    {
        private FlowDocument _document;

        public FlowDocumentWrapper()
        {
        }

        public FlowDocumentWrapper(string title) : base(title)
        {
            var paragraph = new Paragraph(new Run(string.Empty));

            var volume = new Volume();
            volume.Blocks.Add(new Section(paragraph));

            _document = new FlowDocument();
            _document.Blocks.Add(volume);

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

                Children.Clear();
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

        protected override bool CanDeleteComponent()
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
    }
}