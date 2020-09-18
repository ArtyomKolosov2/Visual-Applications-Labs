using Lab_Work_6.Modules;
using Lab_Work_6.View;
using MyContacts.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<Contact> result = new List<Contact>(_contacts.Count);
            foreach (Contact contact in _contacts)
            {
                if (contact.Addres.Contains("Minsk") && contact.MiddleMark >= 4.5)
                {
                    result.Add(contact);
                }
            }
            foreach (var r in result)
            {
                SearchResultTextBox.Text += $"FIO: {r.Fio}, Addres {r.Addres}, Middle Mark {r.MiddleMark}\n";
            }
        }
    }
}

