using Lab_Work_9.ViewModels;
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
using Lab_Work_9.View;
using static System.Math;
using Lab_Work_9.Models;

namespace Lab_Work_9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string TitleText = "Лаб. раб. №9 Ст.Гр. 10701219 Колосов А.А\n";
        public RGBViewModel viewModel { get; set; } = new RGBViewModel(((SolidColorBrush)Application.Current.Resources["BackgroundColorBrush"]).Color);
        public MainWindow()
        {
            InitializeComponent();
            Title = TitleText;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MathFuncGroup.AddNewRadioButtonRange
            (new RadioButtonViewModel[]
                {
                    new RadioButtonViewModel
                    {
                        GetContentText="sh(x)",
                        MathFunction= (double num) => (Pow(E, num) - Pow(E, -num)) / 2d,
                        IsFuncChecked = true
                    },
                    new RadioButtonViewModel{GetContentText="x^2", MathFunction=(double num) => Pow(num, 2) },
                    new RadioButtonViewModel{GetContentText="x^3", MathFunction=(double num) => Pow(num, 3) },
                    new RadioButtonViewModel{GetContentText="e^x", MathFunction=(double num) => Pow(E, num) }
                }
            );
            ResultTextBox.Text += TitleText;
            MenuColorWidget.DataContext = this;
            MenuColorWidget.OkButton.Click += OkButton_Click;
            MenuColorWidget.CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentColor = viewModel.StartColor;
            viewModel.R = viewModel.StartColor.R;
            viewModel.G = viewModel.StartColor.G;
            viewModel.B = viewModel.StartColor.B;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["BackgroundColorBrush"] = new SolidColorBrush(viewModel.CurrentColor);
            viewModel.StartColor = viewModel.CurrentColor;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string text_x1 = GetX1_Input.Text;
            string text_x2 = GetX2_Input.Text;
            string text_n = GetN_Input.Text;
            string text_m = GetM_Input.Text;
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
            if (!int.TryParse(text_m, out int m))
            {
                Is_Succes = false;
                text_m = "Invalid data!";
            }
            ResultTextBox.Clear();
            string resultString =
               $"{TitleText}\n" +
               $"x1 = {text_x1}\n" +
               $"x2 = {text_x2}\n" +
               $"N = {text_n}\n";
            ResultTextBox.Text += resultString;
            if (Is_Succes)
            {
                ResultTextBox.Text += await MathFunctions.StartCountAsync(x1, x2, n, m, MathFuncGroup.UserChoiceMathFunction);
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            var RGB = new RGBColorWidget(viewModel);
            if (RGB.ShowDialog() == true)
            {
                Application.Current.Resources["BackgroundColorBrush"] = new SolidColorBrush(RGB.ResultColor);
                viewModel.StartColor = viewModel.CurrentColor;
            }

        }

    }
}
