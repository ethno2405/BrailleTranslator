using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace BrailleTranslator.Desktop.Controls
{
    public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty BindableDocumentProperty = DependencyProperty.Register("BindableDocument",
                                                                                                         typeof(FlowDocument),
                                                                                                         typeof(BindableRichTextBox),
                                                                                                         new PropertyMetadata(null));

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
            var control = d as BindableRichTextBox;

            if (control == null) return;

            var newValue = e.NewValue as FlowDocument;

            var xaml = XamlWriter.Save(newValue);

            control.Document = XamlReader.Parse(xaml) as FlowDocument;
        }
    }
}