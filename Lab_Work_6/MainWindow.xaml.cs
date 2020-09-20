using Lab_Work_6.Modules;
using Lab_Work_6.View;
using Microsoft.Win32;
using MyContacts.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab_Work_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollectionModifed<Contact> _contacts = new ObservableCollectionModifed<Contact>();
        public string JsonFilePath { get; private set; } = Directory.GetCurrentDirectory() + "/ContactsInfo.json";
        public SearchArgs SearchArgs { get; } = new SearchArgs();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _contacts.CollectionChanged += Collection_Changed;
            DataGridInfo.ItemsSource = _contacts;
            SearchGrid.DataContext = SearchArgs;
        }
        private void AddMarkButton_Clicked(object sender, RoutedEventArgs e)
        {
            Contact data = (Contact)((Button)sender).DataContext;
            MarkDialog dialog = new MarkDialog(new MarkModel());
            if (dialog.ShowDialog() == true)
            {
                data.Marks.Add(dialog.MarkClass);
            }
 
        }
        private void Collection_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                JsonIOservice.WriteToJsonFile(_contacts, JsonFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }
        private void ComboBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MarkModel data = (MarkModel)((ComboBoxItem)sender).DataContext;
            MarkDialog dialog = new MarkDialog(data);
            dialog.ShowDialog();
        }

        private List<Contact> SearchData()
        {
            List<Contact> result = new List<Contact>(_contacts.Count);
            foreach (Contact contact in _contacts)
            {
                if (FindSubString(contact.Addres.ToLower(), SearchArgs.SearchAddres.ToLower()) && contact.MiddleMark >= SearchArgs.SearchMark)
                {
                    result.Add(contact);
                }
            }
            result.Sort(new ContactFIOcomparer());
            return result;
        }

        private bool FindSubString(string mainString, string subString)
        {
            bool searchResult = false;
            if (mainString.Length < subString.Length)
            {
                return searchResult;
            }
            for (int i = 0; i+subString.Length < mainString.Length+1; i++)
            {
                if (mainString.Substring(i, subString.Length).CompareTo(subString) == 0)
                {
                    searchResult = true;
                    break;
                }
            }
            return searchResult;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchArgs.IsAnyMarksError == false)
            {
                List<Contact> result = SearchData();
                StringBuilder resultString = new StringBuilder();
                foreach (var r in result)
                {
                    resultString.Append($"FIO: {r.Fio}, Addres {r.Addres}, Middle Mark {r.MiddleMark}\n");
                }
                SearchResultTextBox.Text = resultString.ToString();
            }
        }

        private void MarkBox_Error(object sender, ValidationErrorEventArgs e)
        {
            SearchArgs.IsAnyMarksError = e.Action == ValidationErrorEventAction.Added ? true : false;
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private async void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Json Files (*.json)|*.json|Txt Files (*.txt)|*.txt"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                await JsonIOservice.WriteToJsonFileAsync(_contacts, saveFileDialog.FileName);
            }
        }
        private async void LoadCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Json Files (*.json)|*.json|Text Files (*.txt)|*.txt*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                _contacts.Clear();
                JsonFilePath = openFileDialog.FileName;
                _contacts.AddContactRange(await JsonIOservice.LoadContactsAsync(JsonFilePath));
            }
        }
        private void LoadCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}

