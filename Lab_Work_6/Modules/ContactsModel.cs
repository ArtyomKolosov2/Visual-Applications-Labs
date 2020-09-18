using Lab_Work_6.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyContacts.Modules
{
    public class ContactFIOcomparer : IComparer<Contact>
    {
        public int Compare(Contact o1, Contact o2)
        {
            return o1.Fio.CompareTo(o2.Fio);
        }
    }

    public class Contact : INotifyPropertyChanged
    {
        private string _fio = null;
        private string _addres = null;
        private double _middle_mark = 0;
        public ObservableCollectionModifed<MarkModel> Marks { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void ContactChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void CountMiddleMark()
        {
            int sum = 0;
            foreach (var mark in Marks)
            {
                sum += mark.Mark;
            }
            MiddleMark = Math.Round((double)sum / Marks.Count, 2);
        }

        public double MiddleMark
        {
            get { return _middle_mark; }
            private set 
            { 
                _middle_mark = value;
                ContactChanged();
            }
        }

        private void Collection_ChangedInvoke(object sender, NotifyCollectionChangedEventArgs e)
        {
            CountMiddleMark();
        }
        public Contact()
        {
            Marks = new ObservableCollectionModifed<MarkModel>();
            Marks.CollectionChanged += Collection_ChangedInvoke;
        }
        public string Fio
        {
            get
            {
                return _fio ?? "No Data";
            }
            set
            {
                _fio = value;
                ContactChanged();
            }
        }

        public string Addres
        {
            get
            {
                return _addres ?? "No Data";
            }
            set
            {
                _addres = value;
                ContactChanged();
            }
        }
    }
}
