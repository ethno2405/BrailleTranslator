using BGAssist.Braille.Client.Logic;
using BGAssist.Braille.Client.Logic.DocumentStructure;
using BGAssist.Braille.Client.Logic.Translation;
using BGAssist.Braille.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Xml.Linq;

namespace BGAssist.Braille.Client.Controls
{
    public partial class TabContent : UserControl
    {
        private const string BraileBoxAutomationPropertiesNameConstPart = "Braile code TextBox. ";

        private bool _isChangeGeneratedFromTextBoxControl;
        private frmMain mainClass;
        
        #region Properties
        public static bool IsAlredyCallProperty
        {
            get;
            set;
        }
        
        #endregion

        public string ClearText
        {
            get
            {
                return new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd).Text;
            }
        }

        private void OnAlignmentButtonClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = (ToggleButton)sender;
            var buttonGroup = new[] { LeftButton, CenterButton, RightButton, JustifyButton };
            this.SetButtonGroupSelection(clickedButton, m_SelectedAlignmentButton, buttonGroup, true);
            m_SelectedAlignmentButton = clickedButton;
        }

        private void OnFontSizeComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Exit if no selection
            if (FontSizeCombo.SelectedItem == null) return;

            // clear selection if value unset
            if (FontSizeCombo.SelectedItem.ToString() == "{DependencyProperty.UnsetValue}")
            {
                FontSizeCombo.SelectedItem = null;
                return;
            }

            // Process selection
            var pointSize = ((ComboBoxItem)FontSizeCombo.SelectedItem).Content;
            var pixelSize = Convert.ToDouble(pointSize) * (96 / 72);
            var textRange = new TextRange(this.TextBox.Selection.Start, this.TextBox.Selection.End);
            textRange.ApplyPropertyValue(TextElement.FontSizeProperty, pixelSize);
        }

        public void SetButtonGroupSelection(ToggleButton clickedButton, ToggleButton currentSelectedButton, IEnumerable<ToggleButton> buttonGroup, bool ignoreClickWhenSelected)
        {
            /* In some cases, if the user clicks the currently-selected button, we want to ignore
             * the click; for example, when a text alignment button is clicked. In other cases, we
             * want to deselect the button, but do nothing else; for example, when a list butteting
             * or numbering button is clicked. The ignoreClickWhenSelected variable controls that
             * behavior. */

            // Exit if currently-selected button is clicked
            if (clickedButton == currentSelectedButton)
            {
                if (ignoreClickWhenSelected) clickedButton.IsChecked = true;
                return;
            }

            // Deselect all buttons
            foreach (var button in buttonGroup)
            {
                button.IsChecked = false;
            }

            // Select the clicked button
            clickedButton.IsChecked = true;
        }

        private void OnListButtonClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = (ToggleButton)sender;
            var buttonGroup = new[] { BulletsButton, NumberingButton };
            this.SetButtonGroupSelection(clickedButton, m_SelectedListButton, buttonGroup, false);
            m_SelectedListButton = clickedButton;
        }

        public static readonly DependencyProperty BrailleDocumentProperty =
            DependencyProperty.Register("BrailleDocument", typeof(FlowDocument),
            typeof(TabContent), new PropertyMetadata(OnBrailleDocumentChanged));

        public static readonly DependencyProperty CodeControlsVisibilityProperty =
            DependencyProperty.Register("CodeControlsVisibility", typeof(Visibility),
            typeof(TabContent));

        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument),
            typeof(TabContent), new PropertyMetadata(OnDocumentChanged));

        public static readonly DependencyProperty ToolbarBackgroundProperty =
            DependencyProperty.Register("ToolbarBackground", typeof(Brush),
            typeof(TabContent));

        public static readonly DependencyProperty ToolbarBorderBrushProperty =
            DependencyProperty.Register("ToolbarBorderBrush", typeof(Brush),
            typeof(TabContent));

        public static readonly DependencyProperty ToolbarBorderThicknessProperty =
            DependencyProperty.Register("ToolbarBorderThickness", typeof(Thickness),
            typeof(TabContent));

        private static ToggleButton m_SelectedAlignmentButton;
        private static ToggleButton m_SelectedListButton;
        private int m_InternalUpdatePending;
        private bool m_TextHasChanged;

        public TabContent()
        {
            InitializeComponent();

            FontFamilyCombo.ItemsSource = FontCollection.FontsCollection;
            FontFamilyCombo.SelectedValuePath = "BrailleFontKey";
            FontFamilyCombo.DisplayMemberPath = "NormalFontValue";
            this.FontFamilyCombo.SelectedIndex = 0;
            this.FontSizeCombo.SelectedIndex = 4;
            this.TextBox.Focus();
            this.AllowDrop = true;
            this.TextBox.FontFamily = FontCollection.FontsCollection.FirstOrDefault().NormalFontValue;

            this.TextBox.SelectionChanged += TextBox_SelectionChanged;
        }

        protected void OnFontFamilyComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyCombo.SelectedItem == null) return;

            var textRange = new TextRange(this.TextBox.Selection.Start, this.TextBox.Selection.End);
            textRange.ApplyPropertyValue(TextElement.FontFamilyProperty, ((FontCollection)FontFamilyCombo.SelectedItem).NormalFontValue);

            // TODO: Delete after time if this piece of code is not required
            var braileRange = new TextRange(this.BraileBox.Selection.Start, this.BraileBox.Selection.End);
            braileRange.ApplyPropertyValue(TextElement.FontFamilyProperty, ((FontCollection)FontFamilyCombo.SelectedItem).BrailleFontKey);
        }

        private void RichTextBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var textToSync = (sender == this.TextBox) ? this.BraileBox : this.TextBox;
            textToSync.ScrollToVerticalOffset(e.VerticalOffset);
            textToSync.ScrollToHorizontalOffset(e.HorizontalOffset);
        }

        private void SetToolbar()
        {
            // Set font family combo
            var textRange = new TextRange(this.TextBox.Selection.Start, this.TextBox.Selection.End);
            var fontFamily = textRange.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamilyCombo.SelectedItem = fontFamily;

            // Set font size combo
            var fontSize = textRange.GetPropertyValue(TextElement.FontSizeProperty);
            FontSizeCombo.Text = fontSize.ToString();

            // Set Font buttons
            if (!String.IsNullOrEmpty(textRange.Text))
            {
                BoldButton.IsChecked = textRange.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold);
                ItalicButton.IsChecked = textRange.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
                UnderlineButton.IsChecked = textRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            }

            // Set Alignment buttons
            LeftButton.IsChecked = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Left);
            CenterButton.IsChecked = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Center);
            RightButton.IsChecked = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Right);
            JustifyButton.IsChecked = textRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).Equals(TextAlignment.Justify);
        }

        private void Copy(RichTextBox control)
        {
            control.Copy();
        }

        private void Cut(RichTextBox control)
        {
            control.Cut();
        }

        private void Undo(RichTextBox control)
        {
            control.Undo();
        }

        private void Redo(RichTextBox control)
        {
            control.Redo();
        }

        private void Paste(RichTextBox control)
        {
            control.Paste();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            this.SetToolbar();
        }

        public FlowDocument BrailleDocument
        {
            get { return (FlowDocument)GetValue(BrailleDocumentProperty); }
            set { SetValue(BrailleDocumentProperty, value); }
        }

        [Browsable(true)]
        [Category("Visibility")]
        [Description("Whether the code controls are visible in the toolbar.")]
        [DefaultValue("Collapsed")]
        public Visibility CodeControlsVisibility
        {
            get { return (Visibility)GetValue(CodeControlsVisibilityProperty); }
            set { SetValue(CodeControlsVisibilityProperty, value); }
        }

        public FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        public RichTextBox TextBoxControl
        {
            get
            {
                return TextBox;
            }
        }

        [Browsable(true)]
        [Category("Brushes")]
        [Description("The background color of the formatting toolbar on the control.")]
        [DefaultValue("Gainsboro")]
        public Brush ToolbarBackground
        {
            get { return (Brush)GetValue(ToolbarBackgroundProperty); }
            set { SetValue(ToolbarBackgroundProperty, value); }
        }

        [Browsable(true)]
        [Category("Brushes")]
        [Description("The color of the formatting toolbar border.")]
        [DefaultValue("Gray")]
        public Brush ToolbarBorderBrush
        {
            get { return (Brush)GetValue(ToolbarBorderBrushProperty); }
            set { SetValue(ToolbarBorderBrushProperty, value); }
        }

        [Browsable(true)]
        [Category("Other")]
        [Description("The thickness of the formatting toolbar border.")]
        [DefaultValue("1,1,1,0")]
        public Thickness ToolbarBorderThickness
        {
            get { return (Thickness)GetValue(ToolbarBorderThicknessProperty); }
            set { SetValue(ToolbarBorderThicknessProperty, value); }
        }

        public void UpdateDocumentBindings()
        {
            if (!m_TextHasChanged) return;
            m_InternalUpdatePending = 2;
            SetValue(DocumentProperty, this.TextBox.Document);
            SetValue(BrailleDocumentProperty, this.BraileBox.Document);
        }

        private static void OnBrailleDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = (TabContent)d;
            FlowDocument fdTarget = new FlowDocument();
            FlowDocument fdSource = thisControl.TextBox.Document;

            StringBuilder sb = new StringBuilder();


            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                XamlDesignerSerializationManager sm = new XamlDesignerSerializationManager(xw);
                sm.XamlWriterMode = XamlWriterMode.Expression;
                XamlWriter.Save(fdSource, sm);
            }
            string copyText = sb.ToString();

            fdTarget = (FlowDocument)XamlReader.Parse(copyText);
            List<Inline> runs = fdTarget.Blocks.OfType<Paragraph>().Select(x => x.Inlines).SelectMany(x => x.Select(y => y)).ToList();

            List<FontFamily> fonts = FontCollection.FontsCollection.Select(x => x.NormalFontValue).ToList();
            foreach (var item in runs.Where(x => fonts.Contains(x.FontFamily) == false))
                item.FontFamily = FontCollection.FontsCollection.FirstOrDefault().BrailleFontKey;

            foreach (FontCollection font in FontCollection.FontsCollection)
                foreach (var item in runs.Where(x => x.FontFamily == font.NormalFontValue))
                    item.FontFamily = font.BrailleFontKey;

            thisControl.BraileBox.Document = fdTarget;
        }

        public string SavedFileName { get; set; }

        public bool IsSaved { get; set; }

        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisControl = (TabContent)d;
            if (thisControl.m_InternalUpdatePending > 0)
            {
                thisControl.m_InternalUpdatePending--;
                return;
            }
            thisControl.TextBox.Document = (e.NewValue == null) ? new FlowDocument() : (FlowDocument)e.NewValue;
            thisControl.m_TextHasChanged = false;
        }

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _isChangeGeneratedFromTextBoxControl = true;
            m_TextHasChanged = true;
            // translate to brail
            UpdateDocumentBindings();
            _isChangeGeneratedFromTextBoxControl = false;

            // create tree structure
            TreeTranslator treeTranslator = new TreeTranslator();
            XmlDocument outputXMLDocument = (XmlDocument)treeTranslator.Convert(this.TextBox.Document, typeof(XmlDocument), null, CultureInfo.InvariantCulture);
            XmlDataProvider provider = (XmlDataProvider)FindResource("treeXMLDocument");
            provider.Document = outputXMLDocument;
            provider.Refresh();

            // TODO: check if the content is changed
            // BrailleTranslator brailleTranslator = new BrailleTranslator();
            // FlowDocument brailleFlowDocument = (FlowDocument)brailleTranslator.Convert(this.TextBox.Document, typeof(FlowDocument), null, CultureInfo.InvariantCulture);
            // BrailleDocument = brailleFlowDocument;

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        public void InsertBlock(BlockType type, String title)
        {
            switch (type)
            {
                case BlockType.Page:
                    //this.TextBox.Document.Blocks.Add(new Section(new Paragraph(new Run())));
                    break;

                case BlockType.Volume:
                    System.Windows.Documents.Section volume = new System.Windows.Documents.Section();
                    volume.Name=title;

                    List<System.Windows.Documents.Section> listSections = new List<System.Windows.Documents.Section>();
                    IEnumerable<System.Windows.Documents.Section> collectionSections = this.TextBox.Document.Blocks.OfType<System.Windows.Documents.Section>();

                    foreach (var sectionb in collectionSections)
                    {
                        listSections.Add(sectionb);                       
                    }
                    
                    foreach (System.Windows.Documents.Section block in listSections)
                    {
                        volume.Blocks.Add(block);//ChildSections.Add(block);....as volume
                    }
                
                    this.TextBox.Document.Blocks.Add(volume);
                    foreach (System.Windows.Documents.Section block in listSections)
                    {
                        this.TextBox.Document.Blocks.Remove(block);
                    }
                
                    this.TextBox.Document.Blocks.LastBlock.Focus();
                    
                    break;


                case BlockType.Section:
                    //BGAssist.Braille.Client.Logic.DocumentStructure.Section section = new BGAssist.Braille.Client.Logic.DocumentStructure.Section();
                    System.Windows.Documents.Section sectiona = new System.Windows.Documents.Section();
                    sectiona.Name = title;

                    List<Block> list = new List<Block>();
                    IEnumerable<Paragraph> collection = this.TextBox.Document.Blocks.OfType<Paragraph>();
                    
                    foreach (var paragraph in collection)
                    {
                        //MessageBox.Show(new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text);
                        //MessageBox.Show(paragraph.Parent.ToString());
                        list.Add(paragraph);  
                    }

                    foreach (Block block in list)
                    {
                        sectiona.Blocks.Add(block);
                    }
                
                    this.TextBox.Document.Blocks.Add(sectiona);
                    this.TextBox.Document.Blocks.LastBlock.Focus();
                    
                    break;

                case BlockType.Paragraph:
                    Paragraph parag = new Paragraph();
                    this.TextBox.Document.Blocks.InsertAfter(this.TextBox.Document.Blocks.LastBlock, parag);
                    this.TextBox.Document.Blocks.LastBlock.Focus();
                    break;
            }
        }

        public void RemoveBlock(BlockType type)
        {
            switch (type)
            {
                case BlockType.Page:
                    //this.TextBox.Document.Blocks.Add(new Section(new Paragraph(new Run())));
                    break;

                case BlockType.Volume:
                    //this.TextBox.Document.Blocks.Add(new Volume(new BGAssist.Braille.Client.Logic.DocumentStructure.Section(new Paragraph(new Run()))));
                    break;

                case BlockType.Section:
                    //this.TextBox.Document.Blocks.Add(new BGAssist.Braille.Client.Logic.DocumentStructure.Section(new Paragraph(new Run())));
                    break;

                case BlockType.Paragraph:
                    //this.TextBox.CaretPosition.InsertParagraphBreak();
                    break;
            }
        }


        public void InsertInline(InlineType inline)
        {
            switch (inline)
            {
                case InlineType.TextRun:
                    this.TextBox.CaretPosition.InsertTextInRun(string.Empty);
                    break;
            }
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            Cut(TextBox);
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Copy(TextBox);
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            Undo(TextBox);
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            Redo(TextBox);
        }

        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            Paste(TextBox);
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BraileBox.Document = Translator.Translate(TextBox.Document);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error: " + ex.Message + "!", "Error");
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (mainClass == null)
            {
                mainClass = new frmMain();
            }
            mainClass.CallShortcutKeys(e, IsAlredyCallProperty);
            IsAlredyCallProperty = true;
        }

        //

        private void BraileBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (mainClass == null)
            {
                mainClass = new frmMain();
            }
            mainClass.CallShortcutKeys(e, IsAlredyCallProperty);
            IsAlredyCallProperty = true;

            //F2 key edit selection in braille code
            if (e.Key == Key.F2 && !String.IsNullOrEmpty(BraileBox.Selection.Text))
            {
                int startIndex = this.BraileBox.Document.ContentStart.GetOffsetToPosition(this.BraileBox.Selection.Start);
                int endIndex = this.BraileBox.Document.ContentStart.GetOffsetToPosition(this.BraileBox.Selection.End);

                TextPointer startTextPointer = this.TextBox.Document.ContentStart.GetPositionAtOffset(startIndex);
                TextPointer endTextPointer = this.TextBox.Document.ContentStart.GetPositionAtOffset(endIndex);

                TextBox.Selection.Select(startTextPointer, endTextPointer);
                TextBox.Focus();
            }

        }

        private void BraileBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BraileBox.SetValue(AutomationProperties.NameProperty, BraileBoxAutomationPropertiesNameConstPart);
        }

        private void BraileBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) //TODO.. Implementing logic for forcing JAWS to read text selection
        {

            //if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.F1)
            //{
            //    var textRange = new TextRange(BraileBox.Selection.Start, BraileBox.Selection.End);
            //    this.BraileBox.SetValue(AutomationProperties.NameProperty, BraileBoxAutomationPropertiesNameConstPart + textRange.Text);
            //    this.BraileBox.Focus();

            //}
        }

        private void BraileBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BraileBox.SetValue(AutomationProperties.NameProperty, BraileBoxAutomationPropertiesNameConstPart + PageManager.FlowDocumentGetText(this.BraileBox.Document));
        }

        public void addVolumeNode_Click(object sender, String volumeTitle)
        {

            //TreeViewItem item = TreeView.ItemContainerGenerator.ContainerFromItem(TreeView.SelectedItem) as TreeViewItem;
            
            try
            {
                InsertBlock(BlockType.Volume, volumeTitle);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error: " + ex.Message + "!", "Error");
            }

        }


        public void addSectionNode_Click(object sender, String sectionTitle, RoutedEventArgs e)
        {
            try
            {
                InsertBlock(BlockType.Section, sectionTitle);
                this.TextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.OnTextChanged);
                //result = methodInfo.Invoke(classInstance, parametersArray);
                //OnBrailleDocumentChanged((DependencyObject)sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error: " + ex.Message + "!", "Error");
            }
        }

        public void addParagraphNode_Click(object sender, String paragraphTitle)
        {
            try
            {
                InsertBlock(BlockType.Paragraph, paragraphTitle);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error: " + ex.Message + "!", "Error");
            }
        }

        public void removeSectionNode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var iner = ((System.Xml.XmlElement)((((System.Windows.Controls.ItemsControl)(this.TreeView)).Items)).Items[0])).InnerXml;
                TreeViewItem tvItem =  BGAssist.Braille.Client.Logic.TreeViewTools.GetItemFromObject(TreeView, sender);//(TreeViewItem)TreeView.SelectedItem;
                //tvItem.
                TreeView.Items.Remove(TreeView.SelectedItem);
                
                /*if (sender is TreeView && ((TreeViewItem)((TreeView)sender).SelectedItem).Parent != null)
                {
                    TreeViewItem parent = (TreeViewItem)((TreeViewItem)((TreeView)sender).SelectedItem).Parent;  // This will give you the parent             
                }
                
                //TreeViewItem treeViewItem = (TreeViewItem)TreeView.SelectedItem;
                //TreeViewItem treeViewItem = TreeView.SelectedItem as TreeViewItem;
                XmlNode treeViewItem = TreeView.SelectedItem as XmlNode;

                if (treeViewItem == null)
                {
                    return;
                }
                //Get the parent of the treeViewItem
                if (treeViewItem.ParentNode is XmlNode)
                {
                    //(treeViewItem as XmlNode).Items.Remove(treeViewItem);
                }
                else
                {
                    //Remove TreeView Item
                    this.TreeView.Items.Remove(treeViewItem);
                }
                /*
                //this.Document.Blocks.Remove(this.Document.Blocks.LastBlock);
                TreeView.Items.RemoveAt(0);
                 int index = TreeView.Items.IndexOf(TreeView.SelectedItem);   
             if(index < 0)
               {
                   index = TreeView.Items.IndexOf(TreeView.SelectedItem);
               }    

            if(index > 0)
              {
                  TreeView.Items.RemoveAt(index);
              } 
                var treeViewItem = TreeView.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
                treeViewItem.IsSelected = false;

                ////TreeView.Items.Remove(TreeView.SelectedItem);
                if (treeViewItem == null)
                {
                    return;
                }
                //Get the parent of the treeViewItem
                if (treeViewItem.Parent is TreeViewItem)
                {
                    (treeViewItem.Parent as TreeViewItem).Items.Remove(treeViewItem);
                }
                else
                {
                    //Remove TreeView Item
                    this.TreeView.Items.Remove(treeViewItem);
                }
                //TreeViewItem item = TreeView.ItemContainerGenerator.ContainerFromItem(TreeView.SelectedItem) as TreeViewItem;
                //item.IsSelected = false;
                //TreeView.Items.Remove(treeNode); */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error: " + ex.Message + "!", "Error");
            }
        }
        private void TreeView_SelectedItemChanged(object sender,
        RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = sender as TreeView;
            if (tree.SelectedValue != null)
            {
               //MessageBox.Show("Selected header: " + tree.SelectedValue.ToString());
            }
                

            // ... Determine type of SelectedItem.
            if (tree.SelectedItem is TreeViewItem)
            {
                // ... Handle a TreeViewItem.
                var item = tree.SelectedItem as TreeViewItem;
                MessageBox.Show("Selected header: " + item.Header.ToString());
                //this.TextBox.Documentle = "Selected header: " + item.Header.ToString();
            }
            else if (tree.SelectedItem is string)
            {
                // ... Handle a string.
                MessageBox.Show("Selected header: " + tree.SelectedItem.ToString());
                //this.Title = "Selected: " + tree.SelectedItem.ToString();
            }
        }

    }
}