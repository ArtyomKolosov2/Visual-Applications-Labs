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
            bool Is_Succes = true;
            if (!double.TryParse(GetX_Input.Text, out double x))
            {
                Is_Succes = false;
            }
            if (!double.TryParse(GetY_Input.Text, out double y))
            {
                Is_Succes = false;
            }
            if (!double.TryParse(GetZ_Input.Text, out double z))
            {
                Is_Succes = false;
            }
            if (Is_Succes)
            {
                ResultTextBox.Clear();
                ResultTextBox.Text += $"a = {CalculateOperation(x, y, z)}";
            }

        }
        
        private double CalculateOperation(double x, double y, double z)
        {
            return Math.Pow(2, -x) * Math.Sqrt(x + Math.Pow(Math.Abs(y), (double)1 / 4)) * Math.Pow(Math.E, (x - 1d / Math.Sin(z)) / 3d);
        }
    }
}
