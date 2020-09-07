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

namespace Lab_Work_3
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text_x1 = GetX1_Input.Text;
            string text_x2 = GetX2_Input.Text;
            string text_n = GetN_Input.Text;
            bool Is_Succes = true;
            if (!double.TryParse(text_x1, out double x1))
            {
                Is_Succes = false;
                text_x1 = "Invalid data!";
            }
            if (!double.TryParse(text_x2, out double x2))
            {
                Is_Succes = false;
                text_x2 = "Invalid data!";
            }
            if (!int.TryParse(text_n, out int n))
            {
                Is_Succes = false;
                text_n = "Invalid data!";
            }
            ResultTextBox.Clear();
            string resultString =
               "Лаб. раб. №1 Ст.Гр. 10701219 Колосов А.А\n" +
               $"x1 = {text_x1}\n" +
               $"x2 = {text_x2}\n" +
               $"N = {text_n}\n";
            if (Is_Succes)
            {
                StartCount(x1, x2, n);
            }
        }
        private void StartCount(double xn, double xk,  int n)
        {
            double h = (xk - xn) / n;
            StringBuilder result_str = new StringBuilder();
            for (int i = 0; i < n; i++, xn+=h)
            {
                xn = Math.Round(xn, 4);
                result_str.Append
                (
                    $"{(i+1).ToString()}. xn = {xn}, S(x) = {Math.Round(GetSXFunctionResult(xn), 4)} \t " +
                    $"Y(x) = {Math.Round(GetYXFunctionResult(xn), 4)}\n"
                );
            }
            ResultTextBox.Text += result_str.ToString();
        }

        private double GetSXFunctionResult(double x)
        {
            double Co = 1;
            double f = Co;
            double sum = f;
            int k = 0;
            while (k < 500)
            {
                double T = -(Math.Pow(x, 2) * (k + 1)) / ((2 * Math.Pow(k, 2) + 1) * (2 * k + 1));
                f *= T;
                sum += f;
                k++;
            }
            return sum;
        }

        private double GetYXFunctionResult(double x)
        {
            return (1 - ((x * x) / 2d)) * Math.Cos(x) - (x / 2d) * Math.Sin(x);
        }

        private int Factorial(int number)
        {
            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
