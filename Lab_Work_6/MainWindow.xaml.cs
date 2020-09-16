using Lab_Work_6.View;
using MyContacts.Modules;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace Lab_Work_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollectionModifed<Contact> _contacts;

        private JsonIOservice _jsonIO = new JsonIOservice();
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
                Close();
                MessageBox.Show(ex.Message + ex.GetType());
            }
            if (_contacts != null)
            {
                _contacts.CollectionChanged += Collection_Changed;
                

                if (_contacts.Count <= 0)
                {
                    _contacts.AddContactRange(CreateRandomContacts.GetContacts(new Random().Next(1, 100)));
                }
                DataGridInfo.ItemsSource = _contacts;
            }
        }
        private void AddMarkButton_Clicked(object sender, RoutedEventArgs e)
        {
            Contact data = (Contact)((Button)sender).DataContext;
            MarkDialog dialog = new MarkDialog();
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

    }
}

