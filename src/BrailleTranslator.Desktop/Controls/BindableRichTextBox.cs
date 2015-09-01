using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using BrailleTranslator.Desktop.Model;

namespace BrailleTranslator.Desktop.Controls
{
    public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty BindableDocumentProperty = DependencyProperty.Register("BindableDocument",
            typeof(FlowDocument),
            typeof(BindableRichTextBox),
            new PropertyMetadata(OnDocumentChanged));

        public static readonly DependencyProperty BindableCaretPositionProperty = DependencyProperty.Register("BindableCaretPosition",
            typeof(TextPointer),
            typeof(BindableRichTextBox),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCaretPositionChanged));

        public FlowDocument BindableDocument
        {
            get
            {
                return (FlowDocument)GetValue(BindableDocumentProperty);
            }
            set
            {
                SetValue(BindableDocumentProperty, value);
            }
        }

        public TextPointer BindableCaretPosition
        {
            get
            {
                return (TextPointer)GetValue(BindableCaretPositionProperty);
            }
            set
            {
                SetValue(BindableCaretPositionProperty, value);
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            var change = e.Changes.FirstOrDefault();

            if (change != null && change.AddedLength == 0 && change.Offset == 0 && change.RemovedLength > 0)
            {
                Document.Blocks.Clear();
            }

            if (!Document.Blocks.All(x => x.GetType() == typeof(Volume)))
            {
                Document.Blocks.Clear();
            }

            if (Document.Blocks.Count == 0)
            {
                Document.Blocks.Add(new Volume(new Section(new Paragraph(new Run(string.Empty)))));
                return;
            }

            BindableDocument = Document;
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            BindableCaretPosition = CaretPosition;
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            BindableCaretPosition = CaretPosition;
        }

        private static void OnCaretPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BindableRichTextBox;

            if (control == null) return;

            control.Focus();
            control.CaretPosition = e.NewValue as TextPointer;
        }

        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var control = d as BindableRichTextBox;

            if (control == null) return;

            control.Document = e.NewValue as FlowDocument;
        }
    }
}