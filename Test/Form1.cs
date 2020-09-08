using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public int[,] array;
        public Form1()
        {
            int size = 5;
            array = new int[size,size];
            InitializeComponent();
            SetColumnsAndRows(size, size);
        }

        private void SetColumnsAndRows(int columns, int rows)
        {
            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = columns;

            Update();
        }

        private void ShowArray<T>(T [,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i,j]+" ");
                }
                Console.Write("\n");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        { 
            array[e.ColumnIndex, e.RowIndex] = Convert.ToInt32(((DataGridView)sender)[e.ColumnIndex, e.RowIndex].Value);
            ShowArray(array);
        }
    }
}
