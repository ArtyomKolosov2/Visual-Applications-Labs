using Lab_Work_11.Views;
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

namespace Lab_Work_11
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
            var FormOne = new Window1();
            FormOne.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var FormTwo = new Window2();
            FormTwo.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var FormThree = new Window3();
            FormThree.Show();
        }
    }
}
