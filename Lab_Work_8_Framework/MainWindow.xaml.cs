using LiveCharts;
using LiveCharts.Wpf;
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
using static System.Math;

namespace Lab_Work_8_Framework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyChart.Series = new SeriesCollection { new LineSeries { Values = new ChartValues<double> { 1, 1, 1, 1 } } };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await StartLogic();
        }

        private async Task StartLogic()
        {
            double x1 = Convert.ToDouble(TextBox_GetX1.Text);
            double x2 = Convert.ToDouble(TextBox_GetX2.Text);
            double h = Convert.ToDouble(TextBox_GetH.Text);
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis { MaxValue = x2, MinValue = x1, Title = "X" });
            var points = await LiveChartVM.GetPointsAsync(x1, x2, h);
            var lineChart = new LineSeries { Values = points, Name = "Graphic" };
            MyChart.Series = new SeriesCollection
            { 
                lineChart, 
                new LineSeries {Values =  new ChartValues<double> { 1, 1, 1, 1 }} 
            };
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
    public static class LiveChartVM
    {
        public static async Task<ChartValues<double>> GetPointsAsync(double x1, double x2, double h)
        {
            return await Task.Run(() => GetPoints(x1, x2, h));
        }
        public static ChartValues<double> GetPoints(double x1, double x2, double h)
        {
            var result = new ChartValues<double>();
            for (;x1 <= x2; x1 += h)
            {
                var calculation = CalculateOperation(x1, 1, 1);
                result.Add(calculation);
            }
            return result;
        }

        private static async Task<double> CalculateOperationAsync(double x, double y, double z)
        {
            return await Task.Run(() => CalculateOperation(x, y, z));
        }
        private static double CalculateOperation(double x, double y, double z)
        {
            return Pow(y, Pow(x, (double)1 / 3)) +
                Pow(Cos(y), 3) *
                (Abs(x - y) * (1d + (Pow(Sin(z), 2)) /
                (Sqrt(x + y))) / (Pow(Math.E, x - y) + (x / 2d)));
        }
    }
}
