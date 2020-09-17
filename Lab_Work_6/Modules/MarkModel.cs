using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_Work_6.Modules
{
    public class MarkModel : INotifyPropertyChanged, IDataErrorInfo
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
                return _subjectName ?? string.Empty; 
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

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Mark":
                        if ((Mark < 0) || (Mark > 10))
                        {
                            error = "Оценка должна быть больше 0 и меньше 10";
                        }
                        break;
                    case "SubjectName":
                        if (SubjectName.Length < 0)
                        {
                            error = "Название предмета должно быть заполнено!";
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
    }
}
