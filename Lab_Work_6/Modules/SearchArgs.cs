using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab_Work_6.Modules
{
    public class SearchArgs : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _searchAddres = "Minsk";
        private double _searchMark = 4.5;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool IsAnyMarksError { get; set; } = false;
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Mark":
                        if ((SearchMark < 0) || (SearchMark > 10))
                        {
                            error = "Оценка должна быть больше 0 и меньше 10";
                        }
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        public string SearchAddres
        {
            get
            {
                return _searchAddres;
            }
            set
            {
                _searchAddres = value;
                OnPropertyChanged();
            }
        }

        public double SearchMark
        {
            get
            {
                return _searchMark;
            }
            set
            {
                if (value > 0 && value < 11)
                {
                    _searchMark = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
