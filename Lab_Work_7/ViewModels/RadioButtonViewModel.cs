using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab_Work_9.ViewModels
{
    public class RadioButtonViewModel : INotifyPropertyChanged
    {
        private string _contentText;

        private static string _groupName = "MathFunctions";

        public bool IsFuncChecked { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate double MathFunctionHandler(double value);
        public MathFunctionHandler MathFunction { get; set; }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string GetContentText 
        {
            get {return _contentText; }
            set 
            { 
                _contentText = value;
                OnPropertyChanged();
            } 
        }
        public static string GetGroupName 
        {
            get { return _groupName; }
        }
    }
}
