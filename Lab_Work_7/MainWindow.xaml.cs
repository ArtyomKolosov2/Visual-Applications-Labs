using Lab_Work_7.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Math;

namespace Lab_Work_7
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

        public ObservableCollection<RadioButtonViewModel> RadioButtons { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RadioButtons = new ObservableCollection<RadioButtonViewModel>()
            {
                new RadioButtonViewModel
                {
                    GetContentText="sh(x)",
                    MathFunction= (double num) => (Pow(E, num) - Pow(E, -num)) / 2d
                },
                new RadioButtonViewModel{GetContentText="x^2", MathFunction=(double num) => Pow(num, 2) },
                new RadioButtonViewModel{GetContentText="x^3", MathFunction=(double num) => Pow(num, 3) },
                new RadioButtonViewModel{GetContentText="e^x", MathFunction=(double num) => Pow(E, num) }
            };
            MathFuncGroup.ListViewRadio.ItemsSource = RadioButtons;
            ResultTextBox.Text += "Лаб. раб. №2 Ст.Гр. 10701219 Колосов А.А\n";
        }
    }
}
