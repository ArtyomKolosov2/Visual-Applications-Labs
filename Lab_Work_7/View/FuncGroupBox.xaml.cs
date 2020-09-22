using Lab_Work_7.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Lab_Work_7.ViewModels.RadioButtonViewModel;

namespace Lab_Work_7.View
{
    /// <summary>
    /// Логика взаимодействия для FuncGroupBox.xaml
    /// </summary>
    public partial class FuncGroupBox : UserControl
    {
        public MathFunctionHandler UserChoiceMathFunction { get; private set; }
        public FuncGroupBox()
        {
            InitializeComponent();
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
    }
}
