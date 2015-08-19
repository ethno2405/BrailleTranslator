using System;
using System.Windows;
using BGAssist.Braille.Client.Controls;
using BGAssist.Braille.Client.Logic.DocumentStructure;
using System.Windows.Controls;


namespace BGAssist.Braille.Client.Forms
{
    public partial class frmNewInput : Window
    {
        public frmNewInput()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            
            if (this.txtTitleText.Text == "")
            {
                MessageBox.Show("You have to set some value!");
                this.DialogResult = true;
                this.Close();

            }

            
            this.DialogResult = true;
            this.Close();
        } 

        private void InitData()
        {
            txtTitleText.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}