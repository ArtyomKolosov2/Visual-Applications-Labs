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
            string text_h = GetH_Input.Text;
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
            if (!double.TryParse(text_n, out double n))
            {
                Is_Succes = false;
                text_n = "Invalid data!";
            }
            if (!double.TryParse(text_h, out double h))
            {
                Is_Succes = false;
                text_n = "Invalid data!";
            }
            ResultTextBox.Clear();
            string resultString =
               "Лаб. раб. №1 Ст.Гр. 10701219 Колосов А.А\n" +
               $"x1 = {text_x1}\n" +
               $"x2 = {text_x2}\n" +
               $"N = {text_n}\n" +
               $"H = {text_h}\n";
            if (Is_Succes)
            {

            }
        }
        private void StartCount(double x1, double x2, double n, double h)
        {

        }
    }
}
