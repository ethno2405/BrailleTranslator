using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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