using Lab_Work_6.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab_Work_6.View
{
    /// <summary>
    /// Логика взаимодействия для MarkDialog.xaml
    /// </summary>
    public partial class MarkDialog : Window
    {
        public MarkModel MarkClass { get; set; } = new MarkModel();
        public MarkDialog()
        {
            InitializeComponent();
        }
    }
}
