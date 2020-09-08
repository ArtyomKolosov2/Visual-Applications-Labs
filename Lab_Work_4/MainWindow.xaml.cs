
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_Work_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] _mainDoubleArray;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            StartResize();
        }
       
        private void StartResize()
        {
            string text_m = SizeTextBoxM.Text;
            string text_n = SizeTextBoxN.Text;
            if (!int.TryParse(text_m, out int m))
            {
                return;
            }
            if (!int.TryParse(text_n, out int n))
            {
                return;
            }
            _mainDoubleArray = new int[m, n];
            MainDataGridView.RowCount = m;
            MainDataGridView.ColumnCount = n;
            for (int i = 0; i < n; i++)
            {
                MainDataGridView.Columns[i].HeaderText = $"i = {i.ToString()}";
            }
            for (int i = 0; i < m; i++)
            {
                MainDataGridView.Rows[i].HeaderCell.Value = $"j = {i.ToString()}";
            }
        }
    }
}
