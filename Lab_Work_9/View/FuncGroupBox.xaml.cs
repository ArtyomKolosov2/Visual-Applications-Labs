using Lab_Work_9.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static Lab_Work_9.ViewModels.RadioButtonViewModel;

namespace Lab_Work_9.View
{
    /// <summary>
    /// Логика взаимодействия для FuncGroupBox.xaml
    /// </summary>
    public partial class FuncGroupBox : UserControl
    {
        public MathFunctionHandler UserChoiceMathFunction { get; private set; } = null;

        public ObservableCollection<RadioButtonViewModel> RadioButtons { get; private set; } = new ObservableCollection<RadioButtonViewModel>();
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

        public void AddNewRadioButtonModel(RadioButtonViewModel viewModel)
        {
            if (UserChoiceMathFunction == null && viewModel.IsFuncChecked)
            {
                UserChoiceMathFunction = viewModel.MathFunction;
            }
            else if (UserChoiceMathFunction != null && viewModel.IsFuncChecked)
            {
                viewModel.IsFuncChecked = false;
            }
            RadioButtons.Add(viewModel);
        }

        public void AddNewRadioButtonRange<T>(T radioButtons) where T: IEnumerable<RadioButtonViewModel>
        {
            foreach (RadioButtonViewModel viewModel in radioButtons)
            {
                AddNewRadioButtonModel(viewModel);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewRadio.ItemsSource = RadioButtons;
        }
    }
}
