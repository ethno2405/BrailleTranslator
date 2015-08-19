using System;
using System.Windows;

namespace BGAssist.Braille.Client.Forms
{
    public partial class frmPageSettings : Window
    {
        public frmPageSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PageCells = Int32.Parse(txtPageWidth.Text);
            Properties.Settings.Default.PageRows = Int32.Parse(txtPageHeight.Text);
            Properties.Settings.Default.PagePadding = Int32.Parse(txtPadding.Text);
            Properties.Settings.Default.HeaderHeight = Int32.Parse(txtHeaderHeight.Text);
            Properties.Settings.Default.ContentHeight = Int32.Parse(txtPageContentHeight.Text);
            
            this.DialogResult = true;
            this.Close();
        } 

        private void InitData()
        {
            txtPageWidth.Text = Properties.Settings.Default.PageCells.ToString( );
            txtPageHeight.Text = Properties.Settings.Default.PageRows.ToString();
            txtPadding.Text = Properties.Settings.Default.PagePadding.ToString();
            txtHeaderHeight.Text = Properties.Settings.Default.HeaderHeight.ToString();
            txtPageContentHeight.Text = Properties.Settings.Default.ContentHeight.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}