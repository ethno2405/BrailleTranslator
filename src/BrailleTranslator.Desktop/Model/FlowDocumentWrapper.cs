using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using BrailleTranslator.Desktop.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace BrailleTranslator.Desktop.Model
{
    public class FlowDocumentWrapper : Component
    {
        private static readonly FlowDocument _defaultDocument = new FlowDocument(new Volume(new Section(new Paragraph(new Run(string.Empty)))));

        private TextPointer _caretPosition;

        private FlowDocument _document;

        private TextSelection _selection;

        private string _preview;

        private FontFamily _previewFont;

        private FontFamily _brailleFont = new FontFamily(new Uri("pack://application:,,,/BrailleTranslator.Desktop;component/fonts/"), "./#Braille Normal");

        public FlowDocumentWrapper()
        {
            DocumentRoot = this;
            SubscribeForMessages();

            TranslateToBraille = new RelayCommand(ExecuteTranslateToBrailleCommand);
        }

        public FlowDocumentWrapper(string title) : base(title)
        {
            _document = _defaultDocument;

            DocumentRoot = this;
            SubscribeForMessages();

            TranslateToBraille = new RelayCommand(ExecuteTranslateToBrailleCommand);
        }

        public ICommand TranslateToBraille { get; private set; }

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
                PreviewFont = _brailleFont;
                Preview = new TextRange(_document.ContentStart, _document.ContentEnd).Text;
            }
        }

        public FontFamily PreviewFont
        {
            get
            {
                return _previewFont;
            }
            set
            {
                Set(nameof(PreviewFont), ref _previewFont, value);
            }
        }

        public override string CreateChildGestureText
        {
            get
            {
                return "Ctrl + Alt + Shift + Enter";
            }
        }

        public string Preview
        {
            get
            {
                return _preview;
            }
            set
            {
                Set(nameof(Preview), ref _preview, value);
            }
        }

        public override string CreateChildText
        {
            get
            {
                return "New volume";
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
                if (Set(nameof(CaretPosition), ref _caretPosition, value))
                {
                    RaisePropertyChanged(nameof(IsCaretInBetween));
                }
            }
        }

        public TextSelection Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                Set(nameof(Selection), ref _selection, value);
            }
        }

        protected override TextPointer ContentStart
        {
            get
            {
                return Document.ContentStart;
            }
        }

        protected override TextPointer ContentEnd
        {
            get
            {
                return Document.ContentEnd;
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

        protected override void CombineComponents(IEnumerable<Component> components)
        {
            throw new NotImplementedException();
        }

        private void ExecuteTranslateToBrailleCommand()
        {
            Messenger.Default.Send(new GenericMessage<string>(new TextRange(Document.ContentStart, Document.ContentEnd).Text), Tokens.Translation);
            PreviewFont = new FontFamily("Segoe UI");
        }

        private void SubscribeForMessages()
        {
            Messenger.Default.Register<KeyShortcutMessage>(this, KeyShortcutMessageToken.Create(Key.Enter, ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt), m => CreateChildCommand.Execute(null));
        }
    }
}