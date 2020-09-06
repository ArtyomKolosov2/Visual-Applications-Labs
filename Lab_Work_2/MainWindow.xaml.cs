using Lab_Work_1_2.modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Lab_Work_1_2.modules.RadioButtonViewModel;

namespace Lab_Work_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MathFunctionHandler UserChoiceMathFunction { get; set; }

        public bool CheckBoxState { get; set; }
        public ObservableCollection<RadioButtonViewModel> RadioButtons { get; private set; }
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
            double? mathFunctionResultNullable = UserChoiceMathFunction?.Invoke(x);
            if (Is_Succes && mathFunctionResultNullable != null)
            {
                double mathFuncResult = mathFunctionResultNullable.Value;
                resultString += $"U(x) = {mathFuncResult}\n";
                resultString += $"m = {CalculateOperation(mathFuncResult, y, z)}\n";
                resultString += $"Max{(CheckBoxState?"Abs":string.Empty)} = {FindMax(mathFuncResult, y, z)}\n";   
            }
            else
            {
                resultString += "U(x) = NaN\n";
                if (mathFunctionResultNullable == null)
                {
                    resultString += "m = Error: Math funtion isn't choosen!\n";
                }
                else
                {
                    resultString += $"m = NaN\n";
                }
                resultString += "Max = NaN\n";
            }
            ResultTextBox.Text += resultString;
        }
        
        private double FindMax(params double[] nums)
        {
            double max = nums[0],
                   num;
            for (int i = 0; i < nums.Length; i++)
            {
                num = nums[i];
                if (CheckBoxState)
                {
                    num = Math.Abs(num);
                }
                if (num > max)
                {
                    max = num;
                }
            }
            return max;
        }
        private double CalculateOperation(double x, double y, double z)
        {
            return (FindMax(x,y,z) / 
                Math.Min(x, y)) + 5d;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButtons = new ObservableCollection<RadioButtonViewModel>()
            {
                new RadioButtonViewModel
                {
                    GetContentText="sh(x)", 
                    MathFunction=(double num) => (Math.Pow(Math.E, num) - Math.Pow(Math.E, -num)) / 2d 
                },
                new RadioButtonViewModel{GetContentText="x^2", MathFunction=(double num) => Math.Pow(num, 2) },
                new RadioButtonViewModel{GetContentText="e^x", MathFunction=(double num) => Math.Pow(Math.E, num)}
            };
            ListViewRadio.ItemsSource = RadioButtons;
            ResultTextBox.Text += "Лаб. раб. №1 Ст.Гр. 10701219 Колосов А.А\n";
            GetX_Input.Text = "0";
            GetY_Input.Text = "0";
            GetZ_Input.Text = "0";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            object newMathFunction = ((RadioButton)sender).DataContext;
            if (newMathFunction is RadioButtonViewModel radioButton)
            {
                if (radioButton == null)
                {
                    throw new NullReferenceException("Error: RadioButtonViewModel doesn't have math function!");
                }
                UserChoiceMathFunction = radioButton.MathFunction;
            }
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBoxState = ((CheckBox)sender).IsChecked.Value;
        }
    }
}
