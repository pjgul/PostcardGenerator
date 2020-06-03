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
using System.IO;


namespace PostcardGenerator
{
    /// Postcard Generetor.
    /// 
    /// 
    /// PG creates postcrads from user input in form of adding images utilizing drag and drop technology and editting texts with various settings.
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// Create postcard
        /// 
        /// The function creates a png file on user's desktop.
        /// It first cheecks whether there is an actual file to be created. If everything is correct it will proceed.
        /// @param bounds Captures the size of the window.
        /// @param renderTarget Creates a bitmap which can be drwan onto by the target.
        /// @param visual Makes rendering possible.
        /// @param fileName is the name of saved file.
        public static void CreateScreenshot(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }


            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            /// The actual image starts its creation here using @param bounds to create a visual of right size.
            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }


            ///The visual is rendered and passed into bitmapEncoder to create an image.
            renderTarget.Render(visual);
            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            /// Finally the image is saved on desktop.
            using (Stream stm = File.Create("C:\\Users\\User\\Desktop\\" + fileName))
            {
                bitmapEncoder.Save(stm);
            }
        }
        

        /// Adds text to the View Screen.
        /// 
        /// The functions copies text from the work screen and adds it to the View screen.
        private void AddText_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = InputBox.Text;
            OutputBox1.Text = InputBox1.Text;
        }
        /// Check if if the cursor had enterted the control.
        /// 
        /// The function checks if the cursor had enterted the boundries of the control and if so changes it to represent that it allows files to be dropped.
        /// Otherwise the cursor remains the same.
        private void TestDrop_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = e.AllowedEffects;
            }
            else
            {
                e.Effects = DragDropEffects.None;

            }
        }

        /// Add files to the Background.
        /// 
        /// The function adds files to the background of View tab through drag-and-drop technology.
        /// @param files A string array to hold dropped images.
        private void TestDrop_Drop(object sender, DragEventArgs e)
        {
            /// @note The file must be a non-null string.
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                e.Data.GetData(DataFormats.FileDrop) is string[] files &&
                files.Length > 0)
            {
                viewImage.Source = new BitmapImage(new Uri(files[0]));
            }
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Ariel font.
        private void Ariel_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Ariel");
            OutputBox.FontFamily = new FontFamily("Ariel");
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Calibri font.
        private void Calibri_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Calibri");
            OutputBox.FontFamily = new FontFamily("Calibri");
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Times New Roman font.
        private void TNR_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Times New Roman");
            OutputBox.FontFamily = new FontFamily("Times New Roman");
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Comis Sans font.
        private void CS_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Comic Sans");
            OutputBox.FontFamily = new FontFamily("Comic Sans");
        }

        /// Text Visibility.
        /// 
        /// The function changes text background visibilty in the View tab to tranparent.
        private void None_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Background = Brushes.Transparent;
            OutputBox1.Background = Brushes.Transparent;
            OutputBox.Opacity = 1;
            OutputBox1.Opacity = 1;
        }
        /// Text Visibility.
        /// 
        /// The function changes text background visibilty in the View tab to a quarter visibility.
        private void Quarter_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Background = Brushes.White;
            OutputBox1.Background = Brushes.White;
            OutputBox.Opacity = 0.25;
            OutputBox1.Opacity = 0.25;
        }
        /// Text Visibility.
        /// 
        /// The function changes text background visibilty in the View tab to a half visibility.
        private void Half_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Background = Brushes.White;
            OutputBox1.Background = Brushes.White;
            OutputBox.Opacity = 0.5;
            OutputBox1.Opacity = 0.5;
        }
        /// Text Visibility.
        /// 
        /// The function changes text background visibilty in the View tab to a three quarters visibility.
        private void ThreeFour_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Background = Brushes.White;
            OutputBox1.Background = Brushes.White;
            OutputBox.Opacity = 0.75;
            OutputBox1.Opacity = 0.75;
        }
        /// Text Visibility.
        /// 
        /// The function changes text background visibilty in the View tab to full visibility.
        private void Full_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Background = Brushes.White;
            OutputBox1.Background = Brushes.White;
            OutputBox.Opacity = 1;
            OutputBox1.Opacity = 1;
        }

        /// Save Screenshot.
        /// 
        /// The function calls the CreateScreenshot(Visual, string) function passing the area of the postacard as the target and file name as fileName.
        private void SaveScreen_Click(object sender, RoutedEventArgs e)
        {
            CreateScreenshot(SaveMe, "Postcard.png");
        }
    }
}
