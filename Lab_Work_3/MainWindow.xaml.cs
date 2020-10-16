using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Math;

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
               "Лаб. раб. №3 Ст.Гр. 10701219 Колосов А.А\n" +
               $"x1 = {text_x1}\n" +
               $"x2 = {text_x2}\n" +
               $"N = {text_n}\n";
            ResultTextBox.Text+= resultString;
            if (Is_Succes)
            {
                StartBtn.IsEnabled = false;
                ResultTextBox.Text += await StartCountAsync(x1, x2, n, m);
                StartBtn.IsEnabled = true;
            }
        }

        private async Task<string> StartCountAsync(double xn, double xk, int n, int m)
        {
            return await Task.Run(() => StartCount(xn, xk, n, m));
        }
        private string StartCount(double xn, double xk,  int n, int m)
        {
            double h = (xk - xn) / m;
            StringBuilder result_str = new StringBuilder();
            for (int i = 1; xn < xk; i++, xn+=h)
            {
                xn = Round(xn, 4);
                result_str.Append
                (
                    $"{i.ToString()}. xn = {xn}, S(x) = {GetSXFunctionResult(xn, n)}\t" +
                    $"Y(x) = {GetYXFunctionResult(xn)}\n"
                );
            }
            return result_str.ToString();
        }

        private double GetSXFunctionResult(double x, int n)
        {
            double sum = 0;
            int k = 0;
            while (k < n)
            {
                sum += CountSXElement(x, k);
                k++;
            }
            return sum;
        }
        private double CountSXElement(double x, int k)
        {
            double result = Pow(-1, k) * ((2d * Pow(k, 2) + 1) / Factorial(2 * k)) * Pow(x, 2 * k);
            return result;
        }
        private long Factorial(int num)
        {
            long result = 1;
            for (int i = 2; i <= num; i++)
            {
                result *= i;
            }
            return result;
        }
        private double GetYXFunctionResult(double x)
        {
            return (1 - ((x * x) / 2d)) * Cos(x) - (x / 2d) * Sin(x);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetN_Input.Text = "100";
        }
    }
}
