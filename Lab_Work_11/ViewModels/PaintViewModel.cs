using Lab_Work_11.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Work_11.ViewModels
{
    public class PaintViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _width;
        private int _height;
        private double _x1;
        private double _x2;
        private double _y1;
        private double _y2;
        private double _startSector;
        private double _endSector;

        private int _inkWidth = 1;
        private bool _isFilled;

        public GeometryEnum Geometry { get; set; } = GeometryEnum.Ellipse;

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public double X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged();
            }
        }

        public double X2
        {
            get => _x2;
            set
            {
                _x2 = value;
                OnPropertyChanged();
            }
        }

        public double Y1
        {
            get => _y1;
            set
            {
                _y1 = value;
                OnPropertyChanged();
            }
        }

        public double Y2
        {
            get => _y2;
            set
            {
                _y2 = value;
                OnPropertyChanged();
            }
        }

        public double EndSector
        {
            get => _endSector;
            set
            {
                if (value > 360)
                {
                    _endSector = Math.Abs(value) % 360;
                }
                else
                {
                    _endSector = Math.Abs(value);
                }
                OnPropertyChanged();
            }
        }

        public int InkWidth
        {
            get => _inkWidth;
            set
            {
                _inkWidth = value;
                OnPropertyChanged();
            }
        }

        public bool IsFilled
        {
            get => _isFilled;
            set
            {
                _isFilled = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
