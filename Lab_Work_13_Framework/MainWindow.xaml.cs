using Lab_Work_12_Framework.ViewModels;
using Lab_Work_9.ViewModels;
using LiveCharts;
using LiveCharts.Wpf;
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

namespace Lab_Work_13_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RGBViewModel RGBViewModel { get; set; } = new RGBViewModel(((SolidColorBrush)Application.Current.Resources["MainBackgroundColor"]).Color);
        public ObservableCollection<DataForGrid> DataForGrids { get; set; } = new ObservableCollection<DataForGrid>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyChart.Series = new SeriesCollection { new LineSeries { Values = new ChartValues<double> { 1, 1, 1, 1 } } };
            MainDataGrid.ItemsSource = DataForGrids;
            ColorWidget.DataContext = this;
            ColorWidget.OkButton.Click += OkButton_Click;
            ColorWidget.CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            RGBViewModel.CurrentColor = RGBViewModel.StartColor;
            RGBViewModel.R = RGBViewModel.StartColor.R;
            RGBViewModel.G = RGBViewModel.StartColor.G;
            RGBViewModel.B = RGBViewModel.StartColor.B;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["MainBackgroundColor"] = new SolidColorBrush(RGBViewModel.CurrentColor);
            RGBViewModel.StartColor = RGBViewModel.CurrentColor;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DataForGrids.Clear();
            await StartLogic();
        }

        private async Task StartLogic()
        {
            double x1 = Convert.ToDouble(TextBox_GetX.Text);
            double x2 = Convert.ToDouble(TextBox_GetX2.Text);
            double h = Convert.ToDouble(TextBox_GetH.Text);
            double y = Convert.ToDouble(TextBox_GetY.Text);
            double z = Convert.ToDouble(TextBox_GetZ.Text);

            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis { MaxValue = x2, MinValue = x1, Title = "X" });
            var points = await GetPointsAsync(x1, x2, h, y, z);
            var lineChart = new LineSeries 
            { 
                Values = points, 
                Name = "Graphic", 
                Title="График расчётов"
            };
            MyChart.Series = new SeriesCollection
            {
                lineChart,
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

        public async Task<ChartValues<double>> GetPointsAsync(double x1, double x2, double h, double y, double z)
        {
            return await Task.Run(() => GetPoints(x1, x2, h, y, z));
        }
        public async Task<ChartValues<double>> GetPoints(double x1, double x2, double h, double y, double z)
        {
            var result = new ChartValues<double>();
            for (; x1 <= x2; x1 += h)
            {
                var calculation = await CalculateOperationAsync(x1, y, z);
                Dispatcher.Invoke(() => DataForGrids.Add(new DataForGrid { X1 = x1, X2 = x2, Y = y, Z = z, A = calculation }));
                result.Add(calculation);
            }
            return result;
        }

        private async Task<double> CalculateOperationAsync(double x, double y, double z)
        {
            return await Task.Run(() => CalculateOperation(x, y, z));
        }
        private double CalculateOperation(double x, double y, double z)
        {
            return Pow(y, Pow(x, (double)1 / 3)) +
                Pow(Cos(y), 3) *
                (Abs(x - y) * (1d + (Pow(Sin(z), 2)) /
                (Sqrt(x + y))) / (Pow(Math.E, x - y) + (x / 2d)));
        }
    }
}
