using Lab_Work_10.modules;
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

namespace Lab_Work_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IniFileReader _iniFilereader = new IniFileReader("config.ini");
        public MainWindow()
        {
            InitializeComponent();
            ReadIniFile();
        }
        private ObservableCollection<string> _strings = new ObservableCollection<string>();

        private void ReadIniFile()
        {
            Width = Convert.ToInt32(_iniFilereader.ReadINI("settings", "Width")); 
            Height = Convert.ToInt32(_iniFilereader.ReadINI("settings", "Height"));
            AddRange(_iniFilereader.ReadINI("settings", "Height").Split(":"));
            

        }
        private void AddRange(IEnumerable<string> collection)
        {
            foreach (var item in collection)
            {
                _strings.Add(item);
            }
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
