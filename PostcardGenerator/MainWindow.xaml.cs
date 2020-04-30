using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostcardGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addText_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Content = InputBox.Text;
            OutputBox1.Content = InputBox1.Text;
        }

        private void DropSpace_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = e.AllowedEffects;
                InputBox.Text = "it works";
            }
            else
            {
                e.Effects = DragDropEffects.None;
                InputBox.Text = "it doesn't work";

            }
        }

        private void DropSpace_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                TestIMage.Source = (files[0]);
                //TestImage.Source = data as BitmapSource;
        }
    }
}
