using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

        public BindableRichTextBox()
        {
            AddHandler(PreviewKeyDownEvent, new RoutedEventHandler(SetCaretPositionToViewModel));
            AddHandler(PreviewMouseDownEvent, new RoutedEventHandler(SetCaretPositionToViewModel));

            BindableCaretPosition = CaretPosition;
        }

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

            if (Document.Blocks.Count == 0)
            {
                Document.Blocks.Add(new Volume(new Section(new Paragraph(new Run(string.Empty)))));
            }

            BindableDocument = Document;
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

        private void SetCaretPositionToViewModel(object sender, EventArgs e)
        {
        }
    }
}