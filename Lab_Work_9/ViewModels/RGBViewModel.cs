using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab_Work_9.ViewModels
{
    public class RGBViewModel : INotifyPropertyChanged
    {
        private byte _r = 0;
        private byte _g = 0;
        private byte _b = 0;
        private Color _currentColor;
        private Color _startColor;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void OnColorPartChanged()
        {
            CurrentColor = Color.FromRgb(R, G, B);
        }
        public RGBViewModel()
        {
            StartColor = Color.FromRgb(0, 0, 0);
            CurrentColor = StartColor;
        }
        public RGBViewModel(Color startColor)
        {
            _r = startColor.R;
            _g = startColor.G;
            _b = startColor.B;
            StartColor = startColor;
            CurrentColor = StartColor;
        }
       public Color StartColor
        {
            get
            {
                return _startColor;
            }
            set
            {
                _startColor = value;
                OnPropertyChanged();
            }
        }
        public Color CurrentColor
        {
            get
            {
                return _currentColor;
            }
            set
            {
                _currentColor = value;
                OnPropertyChanged();
            }
        }
        public byte R
        {
            get
            {
                return _r;
            }
            set
            { 
                _r = value;
                OnPropertyChanged();
                OnColorPartChanged();
            }
        }
        public byte G
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
                OnPropertyChanged();
                OnColorPartChanged();
            }
        }
        public byte B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
                OnPropertyChanged();
                OnColorPartChanged();
            }
        }
    }
}
