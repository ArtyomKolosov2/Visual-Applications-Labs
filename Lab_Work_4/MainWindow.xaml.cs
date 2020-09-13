using System;
using System.Windows;
using System.Windows.Forms;

namespace Lab_Work_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] _mainIntMatrix;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGrid = (DataGridView)sender;
            if (_mainIntMatrix != null)
            {
                int column = e.ColumnIndex, row = e.RowIndex;
                string data = dataGrid[column, row]?.Value?.ToString();
                if (!int.TryParse(data, out _mainIntMatrix[column, row]))
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
            _mainIntMatrix = new int[m, n];
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

        private void Execute_Button(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Clear();
            int minInRow = 0;
            for (int i = 0; i < _mainIntMatrix.GetLength(0); i++)
            {
                minInRow = FindMinInRow(_mainIntMatrix, i);
                for (int j = 0; j < _mainIntMatrix.GetLength(1); j++)
                {
                    if (minInRow == FindMinInColumn(_mainIntMatrix, j))
                    {
                        ResultTextBox.Text += $"Седловая точка {minInRow} в i = {i}, j = {j}\n";
                        break;
                    }
                }
            }
        }

        private int FindMinInColumn(int [,] array, int column)
        {
            int result = array[0, column];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, column] < result)
                {
                    result = array[i, column];
                }
            }
            return result;
        }

        private int FindMinInRow(int [,] array, int row)
        {
            int result = array[row,0];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                if (array[row, i] < result)
                {
                    result = array[row, i];
                }
            }
            return result;
        }
    }
}
