
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
            DataGridView dataGrid = (DataGridView)sender;
            if (_mainDoubleArray != null)
            {
                int column = e.ColumnIndex, row = e.RowIndex;
                string data = dataGrid[column, row]?.Value?.ToString();
                if (!int.TryParse(data, out _mainDoubleArray[column, row]))
                {
                    data = string.Empty;
                }
                dataGrid[column, row].Value = data;
                dataGrid.Refresh();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainDataGridView.AllowUserToAddRows = false;
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            StartResize();
        }
       
        private void StartResize()
        {
            string text_m = SizeTextBoxM.Text;
            string text_n = SizeTextBoxN.Text;
            if (!int.TryParse(text_m, out int m) || !int.TryParse(text_n, out int n))
            {
                return;
            }
            if ((m + n) > ushort.MaxValue || m > ushort.MaxValue || n > ushort.MaxValue)
            {
                SizeTextBoxM.Text = "Error: to much!";
                SizeTextBoxN.Text = "Error: to much!";
                return;
            }
            MainDataGridView.Rows.Clear();
            MainDataGridView.RowCount = m;
            MainDataGridView.ColumnCount = n;    
            _mainDoubleArray = new int[m, n];
            for (int i = 0; i < n; i++)
            {
                MainDataGridView.Columns[i].HeaderText = $"i={i.ToString()}";
            }
            for (int i = 0; i < m; i++)
            {
                MainDataGridView.Rows[i].HeaderCell.Value = $"j={i.ToString()}";
            }
            MainDataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            GC.Collect();
        }
    }
}
