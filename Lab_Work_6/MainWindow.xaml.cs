using Lab_Work_6.Modules;
using Lab_Work_6.View;
using MyContacts.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private ObservableCollectionModifed<Contact> _contacts;
        public SearchArgs SearchArgs { get; } = new SearchArgs();

        private readonly JsonIOservice _jsonIO = new JsonIOservice();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _contacts = _jsonIO.LoadContacts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.GetType());
            }
            if (_contacts != null)
            {
                _contacts.CollectionChanged += Collection_Changed;
                DataGridInfo.ItemsSource = _contacts;
                SearchGrid.DataContext = SearchArgs;
            }
            
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
                _jsonIO.WriteToJsonFile(_contacts);
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
                if (contact.Addres.Contains(SearchArgs.SearchAddres) && contact.MiddleMark >= SearchArgs.SearchMark)
                {
                    result.Add(contact);
                }
            }
            result.Sort(new ContactFIOcomparer());
            return result;
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
    }
}

