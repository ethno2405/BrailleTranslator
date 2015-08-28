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

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (Document.Blocks.Count == 0)
            {
                Document.Blocks.Add(new Volume(new Section(new Paragraph(new Run(string.Empty)))));
            }

            BindableDocument = Document;
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