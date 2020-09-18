﻿using Lab_Work_6.Modules;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyContacts.Modules
{
    public static class CreateRandomContacts
    {
        private static readonly Random random = new Random();
        private static readonly string[] names = new string[]
        {
            "Artyom",
            "Vanya",
            "Egor",
            "Amber",
            "Ilya",
            "Elizabeth",
            "Sam",
            "Vasya",
            "Alice",
            "Cara",
            "Olivia",
            "John",
            "Anna",
            "Sasha",
            null,
            "Emily",
            "Lisa",
            "Petya",
            "Cameron",
            "Zhenya",
            "James",
            "David",
            "Remy",
            "Taylor",
            "Davis",
            "Jennifer",
            "Tarvis",
            "Artour",
            "Gregory",
            null
        };

        private static readonly string[] surNames = new string[]
        {
            "Allyson",
            "Katlinsky",
            "Kolosov",
            "Trump",
            "Einstein",
            "Newton",
            "Svenson",
            "Lennon",
            "Kuzmich",
            null,
            "Ivanchenko",
            "Dale",
            "Dorian",
            "Wilson",
            "McDonald",
            "Cage",
            "Stone",
            "Butler",
            "Butcher",
            "Smith",
            "Jones",
            "House",
            null
        };

        public static Contact[] GetContacts(int amount)
        {
            Contact[] newContacts = new Contact[amount];
            for (int i = 0; i < amount; i++)
            {
                newContacts[i] = new Contact
                {
                    Fio = names[random.Next(names.Length)],
                    Addres = surNames[random.Next(surNames.Length)],
                };
            }
            return newContacts;
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
