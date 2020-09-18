using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MyContacts.Modules
{
    public class ObservableCollectionModifed<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
    {
        public ObservableCollectionModifed()
        {
            CollectionChanged += NewCollectionChange;
        }
        private void NewCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }
        }
        public void AddContactRange<V>(V newData) where V : IEnumerable
        {
            foreach (T newContact in newData)
            {
                if (newContact != null)
                {
                    Add(newContact);
                }
            }
        }
        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs
                (
                NotifyCollectionChangedAction.Replace, 
                sender, 
                sender, 
                IndexOf((T)sender)
                );
            OnCollectionChanged(args);
        }
    }
}
