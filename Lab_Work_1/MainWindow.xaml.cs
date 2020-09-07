using System;
using System.Windows;
using static System.Math;

namespace Lab_Work_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            string text_x = GetX_Input.Text;
            string text_y = GetY_Input.Text;
            string text_z = GetZ_Input.Text;
            bool Is_Succes = true;
            if (!double.TryParse(text_x, out double x))
            {
                Is_Succes = false;
                text_x = "Invalid data!";
            }
            if (!double.TryParse(text_y, out double y))
            {
                Is_Succes = false;
                text_y = "Invalid data!";
            }
            if (!double.TryParse(text_z, out double z))
            {
                Is_Succes = false;
                text_z = "Invalid data!";
            }
            ResultTextBox.Clear();
            string resultString =
               "Лаб. раб. №1 Ст.Гр. 10701219 Колосов А.А\n" +
               $"x = {text_x}\n" +
               $"y = {text_y}\n" +
               $"z = {text_z}\n";
            if (Is_Succes)
            {
                resultString += $"b = {CalculateOperation(x, y, z)}\n";
            }
            else
            {
                resultString += $"b = NaN\n";
            }
            ResultTextBox.Text += resultString;
        }
        
        private double CalculateOperation(double x, double y, double z)
        {
            return Pow(y, Pow(x, (double)1 / 3)) +
                Pow(Cos(y), 3) *
                (Abs(x - y) * (1d + (Pow(Sin(z), 2)) /
                (Sqrt(x + y))) / (Pow(Math.E, x - y) + (x / 2d)));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text += "Лаб. раб. №1 Ст.Гр. 10701219 Колосов А.А\n";
            GetX_Input.Text = "0";
            GetY_Input.Text = "0";
            GetZ_Input.Text = "0";
        }
    }
}
