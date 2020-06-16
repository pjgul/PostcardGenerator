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
    /// PG creates postcrads from user input in form of adding images utilizing drag and drop technology and editting texts with various settings.
    public partial class MainWindow : Window
    {
        /// Operator Selector
        /// 
        /// Ech operation in the calculator tabitem has specific index, addition is 0, subtraction is 1 and so on.
        int operation = 0;

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
            using (Stream stm = File.Create(fileName))
            {
                bitmapEncoder.Save(stm);
            }
        }
        

        /// Adds text to the View Screen.
        /// 
        /// The functions copies text from the work screen and adds it to the View screen.
        public void AddText_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = InputBox.Text;
            OutputBox1.Text = InputBox1.Text;
        }
        /// Check if if the cursor had enterted the control.
        /// 
        /// The function checks if the cursor had enterted the boundries of the control and if so changes it to represent that it allows files to be dropped.
        /// Otherwise the cursor remains the same.
        public void TestDrop_DragEnter(object sender, DragEventArgs e)
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
        public void TestDrop_Drop(object sender, DragEventArgs e)
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
        /// The function changes fonts of the text to Arial font.
        public void Ariel_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Arial");
            OutputBox.FontFamily = new FontFamily("Arial");
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Calibri font.
        public void Calibri_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Calibri");
            OutputBox.FontFamily = new FontFamily("Calibri");
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Times New Roman font.
        public void TNR_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Times New Roman");
            OutputBox.FontFamily = new FontFamily("Times New Roman");
        }

        /// Change Font.
        /// 
        /// The function changes fonts of the text to Comis Sans font.
        public void CS_Click(object sender, RoutedEventArgs e)
        {
            InputBox.FontFamily = new FontFamily("Comic Sans MS");
            OutputBox.FontFamily = new FontFamily("Comic Sans MS");
        }

       

        /// Save Screenshot.
        /// 
        /// The function calls the CreateScreenshot(Visual, string) function passing the area of the postacard as the target and file name as fileName.
        public void SaveScreen_Click(object sender, RoutedEventArgs e)
        {
            CreateScreenshot(SaveMe, "Postcard.png");
        }

        /// Change icon add.
        /// 
        /// Changes icon for calculator to addition.
        public void PlusItem_Click(object sender, RoutedEventArgs e)
        {
            CalcType.Content = "+";
            operation = 0;
        }
        /// Change icon subtract.
        /// 
        /// Changes icon for calculator to subtraction.
        public void SubtItem_Click(object sender, RoutedEventArgs e)
        {
            CalcType.Content = "-";
            operation = 1;
        }
        /// Change icon multiply.
        /// 
        /// Changes icon for calculator to multiplication.
        public void MultipItem_Click(object sender, RoutedEventArgs e)
        {
            CalcType.Content = "*";
            operation = 2;
        }
        /// Change icon divide.
        /// 
        /// Changes icon for calculator to divsion.
        public void DivItem_Click(object sender, RoutedEventArgs e)
        {
            CalcType.Content = "/";
            operation = 3;
        }

        /// Calculates the result based on the operation.
        /// 
        /// The function uses the switch to determine which type of calculation is being used per operation.
        /// Then it checks if the parameter isn't a string with CanParse(string).
        /// If it's not a string the function gives the result of the calculation.
        /// If it is a string, it displays an error message.
        /// @param operation
        public void Calculate_Click(object sender, RoutedEventArgs e)
        {
            switch(operation)
            {
                case 0:
                    {
                        if (CanParse(Calc1.Text) && CanParse(Calc2.Text))
                        {
                            Eval.Text = "" + (double.Parse(Calc1.Text) + double.Parse(Calc2.Text));
                            ErrorLabel.Content = "";
                        }
                        else
                            ErrorLabel.Content = "Error: You used letters instead of numbers.";
                        break;
                    }
                case 1:
                    {
                        if (CanParse(Calc1.Text) && CanParse(Calc2.Text))
                        {
                            Eval.Text = "" + (double.Parse(Calc1.Text) - double.Parse(Calc2.Text));
                            ErrorLabel.Content = "";
                        }
                        else
                            ErrorLabel.Content = "Error: You used letters instead of numbers.";
                        break;
                    }
                case 2:
                    {
                        if (CanParse(Calc1.Text) && CanParse(Calc2.Text))
                        {
                            Eval.Text = "" + (double.Parse(Calc1.Text) * double.Parse(Calc2.Text));
                            ErrorLabel.Content = "";
                        }
                        else
                            ErrorLabel.Content = "Error: You used letters instead of numbers.";
                        break;
                    }
                case 3:
                    {
                        if (CanParse(Calc1.Text) && CanParse(Calc2.Text))
                        {
                            Eval.Text = "" + (double.Parse(Calc1.Text) / double.Parse(Calc2.Text));
                            ErrorLabel.Content = "";
                        }
                        else
                            ErrorLabel.Content = "Error: You used letters instead of numbers.";
                        break;
                    }
            }
            
        }

        /// Cotinue function.
        /// 
        /// Sets the result of the calculation in the first textbox.
        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            Calc1.Text = Eval.Text;
        }

        /// Checks if zero.
        /// 
        /// The function checks if number given is zero, if it is it returns true, otherwise it returns false.
        /// @returns bool
        public static bool IsRight(double right)
        {
            if (right == 0)
                return true;
            else
                return false;
        }

        /// Check if string.
        /// 
        /// The function checks whether the given string can be parsed into double.
        /// If not, returns false.
        /// @returns bool
        public static bool CanParse(string str)
        {
            bool test = Double.TryParse(str, out double hold);
            if (test)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
