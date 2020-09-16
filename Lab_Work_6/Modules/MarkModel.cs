using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab_Work_6.Modules
{
    public class MarkModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string name=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private int _mark;
        private string _subjectName;

        public string StringRepr
        {
            get { return $"{SubjectName} : {Mark}"; }
        }

        public string SubjectName
        {
            get 
            { 
                return _subjectName ?? "No Data"; 
            }
            set
            {
                _subjectName = value;
                OnPropertyChanged();
            }
        }

        public int Mark
        {
            get
            {
                return _mark;
            }
            set
            {
                if (value > 0  && value < 11)
                {
                    _mark = value;
                    OnPropertyChanged();
                }
                
            }
        }

    }
}
