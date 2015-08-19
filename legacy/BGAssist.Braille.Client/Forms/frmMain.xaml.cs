using BGAssist.Braille.Client.Controls;
using BGAssist.Braille.Client.Forms;
using BGAssist.Braille.Client.Logic;
using BGAssist.Braille.Client.Logic.Translation;
using BGAssist.Braille.Client.Logic.DocumentStructure;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Automation;
using BGAssist.Braille.Client.Properties;
using System.Text;
using iTextSharp.text.pdf;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BGAssist.Braille.Client
{
    public partial class frmMain : Window
    {
        TraversalRequest _tRequest;
        UIElement _focusedElement;

        #region Methods
        public frmMain()
        {
            InitializeComponent();

            this.CurrentTab.TextBox.TextChanged += delegate { DetectIfDocumentIsChanged(); };
        }

        public TabContent CurrentTab
        {
            get
            {
                if (Tabs.SelectedItem == null)
                {
                    TabItem tab = new TabItem();
                    tab.Header = "Document";
                    tab.Content = new TabContent();
                    Tabs.Items.Add(tab);
                    Tabs.SelectedItem = tab;
                }

                TabItem currentTab = (TabItem)Tabs.SelectedItem;
                return (TabContent)currentTab.Content;
            }
        }

        private void AddNewTabItem()
        {
            try
            {
                TabItem newTab = new TabItem();
                ContextMenu menu = new ContextMenu();
                MenuItem closeOption = new MenuItem();
                TabContent content = new TabContent();

                closeOption.Header = "Close";
                closeOption.Click += delegate { RemoveTabItem(newTab); };

                menu.Items.Add(closeOption);

                newTab.Header = "Document";
                newTab.Content = content;
                newTab.ContextMenu = menu;

                Tabs.Items.Add(newTab);
                Tabs.SelectedItem = newTab;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DetectIfDocumentIsChanged()
        {
            this.CurrentTab.IsSaved = false;
            if (((TabItem)this.Tabs.SelectedItem).Header.ToString().LastOrDefault() != '*')
                ((TabItem)this.Tabs.SelectedItem).Header += "*";
        }

        private void RemoveTabItem(TabItem item)
        {
            if (item != null)
            {
                Tabs.Items.Remove(item);
            }
        }

        private void SaveDocument(string fileName)
        {
            FileStream xamlFile = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            XamlWriter.Save(this.CurrentTab.TextBox.Document, xamlFile);
            xamlFile.Close();
        }

        public void CallShortcutKeys(System.Windows.Input.KeyEventArgs e, bool isAlrearyCall)
        {
            Key altKey = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (isAlrearyCall == false)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    switch (e.Key)
                    {
                        case Key.N:
                            MenuItem_Click_New(this, null);
                            e.Handled = true;
                            break;
                        case Key.O:
                            MenuItem_Click_Open(this, null);
                            e.Handled = true;
                            break;
                        case Key.S:
                            MenuItem_Click_Save(this, null);
                            e.Handled = true;
                            break;
                        case Key.A:
                            MessageBox.Show("Ctrl + A");
                            e.Handled = true;
                            break;
                        case Key.OemTilde:
                            MessageBox.Show("Ctrl + `");
                            e.Handled = true;
                            break;
                        case Key.D0:
                            MessageBox.Show("Ctrl + 0");
                            e.Handled = true;
                            break;
                        case Key.F:
                            MessageBox.Show("Ctrl + F");
                            e.Handled = true;
                            break;
                        case Key.P:
                            MessageBox.Show("Ctrl + P");
                            e.Handled = true;
                            break;
                        case Key.E:
                            MessageBox.Show("Ctrl + E");
                            e.Handled = true;
                            break;
                        case Key.R:
                            MessageBox.Show("Ctrl + R");
                            e.Handled = true;
                            break;
                        case Key.Z:
                            MessageBox.Show("Ctrl + Z");
                            e.Handled = true;
                            break;
                        case Key.Y:
                            MessageBox.Show("Ctrl + Y");
                            e.Handled = true;
                            break;
                        case Key.X:
                            MessageBox.Show("Ctrl + X");
                            e.Handled = true;
                            break;
                        case Key.C:
                            MessageBox.Show("Ctrl + C");
                            e.Handled = true;
                            break;
                        case Key.V:
                            MessageBox.Show("Ctrl + V");
                            e.Handled = true;
                            break;
                        case Key.K:
                            MessageBox.Show("Ctrl + K");
                            e.Handled = true;
                            break;
                        case Key.F3:
                            MessageBox.Show("Ctrl + F3");
                            e.Handled = true;
                            break;
                        case Key.B:
                            MessageBox.Show("Ctrl + B");
                            e.Handled = true;
                            break;
                        case Key.J:
                            MessageBox.Show("Ctrl + J");
                            e.Handled = true;
                            break;
                        case Key.H:
                            MessageBox.Show("Ctrl + H");
                            e.Handled = true;
                            break;
                        case Key.NumPad1:
                            MessageBox.Show("Ctrl + NumPad1");
                            e.Handled = true;
                            break;
                        case Key.NumPad2:
                            MessageBox.Show("Ctrl + NumPad2");
                            e.Handled = true;
                            break;
                        case Key.NumPad3:
                            MessageBox.Show("Ctrl + NumPad3");
                            e.Handled = true;
                            break;
                        case Key.PageDown:
                            MessageBox.Show("Ctrl + PageDown");
                            e.Handled = true;
                            break;
                        case Key.PageUp:
                            MessageBox.Show("Ctrl + PageUp");
                            e.Handled = true;
                            break;
                        case Key.Tab:
                            MessageBox.Show("Ctrl + Tab");
                            e.Handled = true;
                            break;
                        case Key.Delete:
                            MessageBox.Show("Ctrl + Delete");
                            e.Handled = true;
                            break;
                        case Key.Enter:
                            MessageBox.Show("Ctrl + Enter");
                            e.Handled = true;
                            break;
                        case Key.F6:
                            _tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                            _focusedElement = Keyboard.FocusedElement as UIElement;

                            if (_focusedElement != null)
                            {
                                _focusedElement.MoveFocus(_tRequest);
                            }
                            e.Handled = true;
                            break;
                    }
                }
                else if ((Keyboard.Modifiers & (ModifierKeys.Alt | ModifierKeys.Shift | ModifierKeys.Control)) == (ModifierKeys.Alt | ModifierKeys.Shift | ModifierKeys.Control))
                {
                    switch (e.Key)
                    {
                        case Key.Enter:
                            //MessageBox.Show("Ctrl + Alt + Shift + Enter");
                            MenuItem_Click_NewVolume(this,null);
                            e.Handled = true;
                            break;
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    switch (e.Key)
                    {
                        case Key.F3:
                            MessageBox.Show("Shift + F3");
                            e.Handled = true;
                            break;
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Alt)
                {
                    switch (altKey)
                    {
                        case Key.D1:
                            MessageBox.Show("Alt + 1");
                            e.Handled = true;
                            break;
                        case Key.D2:
                            MessageBox.Show("Alt + 2");
                            e.Handled = true;
                            break;
                        case Key.D3:
                            MessageBox.Show("Alt + 3");
                            e.Handled = true;
                            break;
                        case Key.D4:
                            MessageBox.Show("Alt + 4");
                            e.Handled = true;
                            break;
                        case Key.D5:
                            MessageBox.Show("Alt + 5");
                            e.Handled = true;
                            break;
                        case Key.D6:
                            MessageBox.Show("Alt + 6");
                            e.Handled = true;
                            break;
                        case Key.D7:
                            MessageBox.Show("Alt + 7");
                            e.Handled = true;
                            break;
                        case Key.D8:
                            MessageBox.Show("Alt + 8");
                            e.Handled = true;
                            break;
                        case Key.D9:
                            MessageBox.Show("Alt + 9");
                            e.Handled = true;
                            break;
                        case Key.D0:
                            MessageBox.Show("Alt + 10");
                            e.Handled = true;
                            break;
                    }
                }
                else if (e.Key == Key.F6)
                {
                    _tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                    _focusedElement = Keyboard.FocusedElement as UIElement;

                    if (_focusedElement != null)
                    {
                        _focusedElement.MoveFocus(_tRequest);
                    }
                    e.Handled = true;
                }
                else if ((Keyboard.Modifiers & (ModifierKeys.Alt | ModifierKeys.Shift)) == (ModifierKeys.Alt | ModifierKeys.Shift))
                {
                    switch (altKey)
                    {
                        case Key.D1:
                            MessageBox.Show("Shift + Alt + 1");
                            e.Handled = true;
                            break;
                        case Key.D2:
                            MessageBox.Show("Shift + Alt + 2");
                            e.Handled = true;
                            break;
                        case Key.D3:
                            MessageBox.Show("Shift + Alt + 3");
                            e.Handled = true;
                            break;
                        case Key.D4:
                            MessageBox.Show("Shift + Alt + 4");
                            e.Handled = true;
                            break;
                        case Key.D5:
                            MessageBox.Show("Shift + Alt + 5");
                            e.Handled = true;
                            break;
                        case Key.D6:
                            MessageBox.Show("Shift + Alt + 6");
                            e.Handled = true;
                            break;
                        case Key.D7:
                            MessageBox.Show("Shift + Alt + 7");
                            e.Handled = true;
                            break;
                        case Key.D8:
                            MessageBox.Show("Shift + Alt + 8");
                            e.Handled = true;
                            break;
                        case Key.D9:
                            MessageBox.Show("Shift + Alt + 9");
                            e.Handled = true;
                            break;
                        case Key.D0:
                            MessageBox.Show("Shift + Alt + 10");
                            e.Handled = true;
                            break;
                    }
                }
                else if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
                {
                    switch (e.Key)
                    {
                        case Key.D1:
                            MessageBox.Show("Shift+Ctrl+ 1");
                            e.Handled = true;
                            break;
                        case Key.D2:
                            MessageBox.Show("Shift+Ctrl+ 2");
                            e.Handled = true;
                            break;
                        case Key.D3:
                            MessageBox.Show("Shift+Ctrl+ 3");
                            e.Handled = true;
                            break;
                        case Key.S:
                            MessageBox.Show("Shift+Ctrl+ S");
                            e.Handled = true;
                            break;
                        case Key.C:
                            MessageBox.Show("Shift+Ctrl+ C");
                            e.Handled = true;
                            break;
                        case Key.F:
                            MessageBox.Show("Shift+Ctrl+ F");
                            e.Handled = true;
                            break;
                        case Key.V:
                            MessageBox.Show("Shift+Ctrl+ V");
                            e.Handled = true;
                            break;
                        case Key.L:
                            MessageBox.Show("Shift+Ctrl+ L");
                            e.Handled = true;
                            break;
                        case Key.P:
                            MessageBox.Show("Shift+Ctrl+ P");
                            e.Handled = true;
                            break;
                        case Key.B:
                            MessageBox.Show("Shift+Ctrl+ B");
                            e.Handled = true;
                            break;
                        case Key.J:
                            MessageBox.Show("Shift+Ctrl+ J");
                            e.Handled = true;
                            break;
                        case Key.OemTilde:
                            MessageBox.Show("Ctrl+Shift + `");
                            e.Handled = true;
                            break;
                        case Key.K:
                            MessageBox.Show("Shift+Ctrl+ K");
                            e.Handled = true;
                            break;
                        case Key.F3:
                            MessageBox.Show("Shift+Ctrl+ F3");
                            e.Handled = true;
                            break;
                        case Key.Enter:
                            //MessageBox.Show("Shift+Ctrl+ Enter");
                            //MenuItem_Click_NewSection(this, null);
                            e.Handled = true;
                            break;
                        case Key.Delete:
                            MessageBox.Show("Shift+Ctrl+ Delete");
                            MenuItem_Click_DeleteSection(this, null);
                            e.Handled = true;
                            break;
                        case Key.LeftAlt:
                            MessageBox.Show("Shift+Ctrl+ Alt");
                            e.Handled = true;
                            break;
                        case Key.Tab:
                            MessageBox.Show("Shift+Ctrl+ Tab");
                            e.Handled = true;
                            break;

                    }
                }
                else if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Alt)) == (ModifierKeys.Control | ModifierKeys.Alt))
                {
                    switch (e.Key)
                    {
                        case Key.D1:
                            MessageBox.Show("Ctrl + Alt + 1");
                            e.Handled = true;
                            break;
                        case Key.D2:
                            MessageBox.Show("Ctrl + Alt + 2");
                            e.Handled = true;
                            break;
                        case Key.D3:
                            MessageBox.Show("Ctrl + Alt + 3");
                            e.Handled = true;
                            break;
                        case Key.D4:
                            MessageBox.Show("Ctrl + Alt + 4");
                            e.Handled = true;
                            break;
                        case Key.D5:
                            MessageBox.Show("Ctrl + Alt + 5");
                            e.Handled = true;
                            break;
                        case Key.D6:
                            MessageBox.Show("Ctrl + Alt + 6");
                            e.Handled = true;
                            break;
                        case Key.D7:
                            MessageBox.Show("Ctrl + Alt + 7");
                            e.Handled = true;
                            break;
                        case Key.D8:
                            MessageBox.Show("Ctrl + Alt + 8");
                            e.Handled = true;
                            break;
                        case Key.D9:
                            MessageBox.Show("Ctrl + Alt + 9");
                            e.Handled = true;
                            break;
                        case Key.D0:
                            MessageBox.Show("Ctrl + Alt + 10");
                            e.Handled = true;
                            break;
                        case Key.PageDown:
                            MessageBox.Show("Ctrl + Alt + PageDown");
                            e.Handled = true;
                            break;
                        case Key.PageUp:
                            MessageBox.Show("Ctrl + Alt + PageUp");
                            e.Handled = true;
                            break;
                    }
                }
            }
            TabContent.IsAlredyCallProperty = false;
        }
        #endregion

        #region Events
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Click_Save(this, null);

            Environment.Exit(0);
        }

        private void Export_Document(object sender, RoutedEventArgs e)
        {
            // TODO: Export as DOC and PDF
            /*
            TextDocument doc = new TextDocument();
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                PdfWriter.GetInstance(doc, new FileStream(dialog.SavedFileName, FileMode.Create));

                BaseFont baseFont = BaseFont.CreateFont(@"C:\BRAILLE1.ttf".ToString(), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                TextRange txt = new TextRange(this.Tab.TextBox.TextDocument.ContentStart, this.Tab.TextBox.TextDocument.ContentEnd);
                try
                {
                    doc.Open();
                    iTextSharp.text.Paragraph pr1 = new iTextSharp.text.Paragraph(txt.Text, font);
                    doc.Add(pr1);
                    doc.Close();
                    MessageBox.Show("PDF-файл успешно създаден");
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
            }*/
        }

        public void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                // call menuPlay_Click
                MenuItem_Click_NewSection(sender, e);
            }
        }
        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            AddNewTabItem();
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.Title = "Open Publication";
            dlg.Filter = "Braille Files (*.brf)|*.brf|Text Files (*.txt)|*.txt";
            if (dlg.ShowDialog() == true)
            {
                string ext = Path.GetExtension(dlg.FileName);
                if (ext == ".brf")
                {
                FileStream xamlFile = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                this.CurrentTab.TextBox.Document = XamlReader.Load(xamlFile) as FlowDocument;
                this.CurrentTab.IsSaved = true;
                this.CurrentTab.SavedFileName = dlg.FileName;
                xamlFile.Close();
            }
                else if (ext == ".txt")
                {
                    using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        TextRange textBoxRange = new TextRange(this.CurrentTab.TextBox.Document.ContentStart, this.CurrentTab.TextBox.Document.ContentEnd);
                        textBoxRange.Load(fs, System.Windows.DataFormats.Text);
                        this.CurrentTab.IsSaved = true;
                        this.CurrentTab.SavedFileName = dlg.FileName;
        }
                }
            }
        }

        private void MenuItem_Click_Options(object sender, RoutedEventArgs e)
        {
            frmPageSettings settings = new frmPageSettings();
            if (settings.ShowDialog() == true)
            {
                MessageBox.Show("You have set the page settings!");
            }
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.DefaultExt = ".braille";
            dlg.Filter = "Braille Files (*.brf)|*.brf";
            dlg.Title = "Save Publication";

            if (this.CurrentTab.IsSaved == false)
            {
                if (string.IsNullOrEmpty(this.CurrentTab.SavedFileName))
                {
                    if (dlg.ShowDialog() == true)
                    {
                        this.CurrentTab.SavedFileName = dlg.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                SaveDocument(this.CurrentTab.SavedFileName);
                this.CurrentTab.IsSaved = true;
                if (((TabItem)this.Tabs.SelectedItem).Header.ToString().LastOrDefault() == '*')
                    ((TabItem)this.Tabs.SelectedItem).Header = ((TabItem)this.Tabs.SelectedItem).Header.ToString().Substring(0, ((TabItem)this.Tabs.SelectedItem).Header.ToString().Length - 1);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.DefaultExt = ".braille";
            dlg.Filter = "Braille Files (*.brf)|*.brf";
            dlg.Title = "Save Publication";

            if (dlg.ShowDialog() == true)
            {
                this.CurrentTab.SavedFileName = dlg.FileName;
            }
            else
            {
                return;
            }

            SaveDocument(this.CurrentTab.SavedFileName);
            this.CurrentTab.IsSaved = true;
            if (((TabItem)this.Tabs.SelectedItem).Header.ToString().LastOrDefault() == '*')
                ((TabItem)this.Tabs.SelectedItem).Header = ((TabItem)this.Tabs.SelectedItem).Header.ToString().Substring(0, ((TabItem)this.Tabs.SelectedItem).Header.ToString().Length - 1);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CallShortcutKeys(e, TabContent.IsAlredyCallProperty);
        }  //TODO..

        private void SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            // Alwasy focus TextBox
            this.CurrentTab.TextBox.Focus();
        }

        private void MenuItem_Click_Print(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                pd.PrintVisual(this.CurrentTab.BraileBox as Visual, "Printing Braille Document");
                pd.PrintDocument((((IDocumentPaginatorSource)this.CurrentTab.BraileBox.Document).DocumentPaginator), "printing as Paginator");
            }
        }

        private void MenuItem_Click_Preferences(object sender, RoutedEventArgs e)
        {
            frmPageSettings frm = new frmPageSettings();
            if (frm.ShowDialog() == true)
            {
                //TODO..
            }
            else
            {
                //TODO..
            }

        }

        private void MenuItem_Click_F6(object sender, RoutedEventArgs e)
        {
            _tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            _focusedElement = Keyboard.FocusedElement as UIElement;

            if (_focusedElement != null)
            {
                _focusedElement.MoveFocus(_tRequest);
            }
            e.Handled = true;

        }

        private void MenuItem_Click_Embos(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Convert(object sender, RoutedEventArgs e)
        {
            try
            {
                this.CurrentTab.BraileBox.Document = Translator.Translate(this.CurrentTab.TextBox.Document);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error: " + ex.Message + "!", "Error");
            }
        }
        private void MenuItem_Click_NewVolume(object sender, RoutedEventArgs e)
        {
            
            frmNewInput settings = new frmNewInput();
            if (settings.ShowDialog() == true)
            {
                MessageBox.Show("You have set the new volume name: " + settings.txtTitleText.Text);
            }
            this.CurrentTab.addVolumeNode_Click(sender, settings.txtTitleText.Text);
        }

        private void MenuItem_Click_NewParagraph(object sender, RoutedEventArgs e)
        {

            frmNewInput settings = new frmNewInput();
            if (settings.ShowDialog() == true)
            {
                MessageBox.Show("You have set the new paragraph name: " + settings.txtTitleText.Text);
            }
            //Paragraph paragraph = new Paragraph();
            this.CurrentTab.addParagraphNode_Click(sender, settings.txtTitleText.Text);
        }

        private void MenuItem_Click_NewSection(object sender, RoutedEventArgs e)
        {

            frmNewInput settings = new frmNewInput();
            if (settings.ShowDialog() == true)
            {
                MessageBox.Show("You have set the new section name: " + settings.txtTitleText.Text);
            }
            this.CurrentTab.addSectionNode_Click(sender, settings.txtTitleText.Text, e);
        }

        private void MenuItem_Click_DeleteSection(object sender, RoutedEventArgs e)
        {
            //TreeViewItem item = TreeView.ItemContainerGenerator.ContainerFromItem(TreeView.SelectedItem) as TreeViewItem;
            this.CurrentTab.removeSectionNode_Click(sender, e);
        }

        private void CommandBinding_Executed(Object sender, ExecutedRoutedEventArgs e)
        {
            MenuItem_Click_NewSection(this,e);
        }

        private void CommandBinding_CanExecute(Object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        
    }
}