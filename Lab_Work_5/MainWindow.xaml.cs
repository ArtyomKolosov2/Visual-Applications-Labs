﻿using System;
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

namespace Lab_Work_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _strings = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StringComboBox.ItemsSource = _strings;
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string text = StringComboBox.Text;
            if (text.Length > 0)
            {
                StringComboBox.Text = string.Empty;
                _strings.Add(text.Trim());
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StringComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            string item = comboBox?.SelectedItem?.ToString();
            if (item != null) 
            {
                ResultTextBox.Clear();
                string[] words = item.Split();
                Array.Sort(words);
                ResultTextBox.Text = string.Join('\n', words);
            }
        }

        private void StringComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            /*List<string> vs = new List<string>()
            {
                "Kolosov AA",
                "Kuzmich Ivan",
                "Kramsaev Volodya",
                "Nikita Vershinin"
            };
            StringComboBox.ItemsSource = vs;*/
        }
    }  
}
