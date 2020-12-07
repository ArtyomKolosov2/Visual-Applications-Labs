using Lab_Work_11.Enum;
using Lab_Work_11.ViewModels;
using Lab_Work_9.View;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Lab_Work_11.Views
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public RGBViewModel ColorViewModel { get; set; } = new();
        public PaintViewModel PaintViewModel { get; set; } = new();
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeometryChoiceLabel.Content = "Выбрано: Rectangle";
            PaintViewModel.Geometry = GeometryEnum.Rectangle;
            TextBox_InputX2.IsReadOnly = true;
            TextBox_InputY2.IsReadOnly = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GeometryChoiceLabel.Content = "Выбрано: Line";
            PaintViewModel.Geometry = GeometryEnum.Line;
            TextBox_InputX2.IsReadOnly = false;
            TextBox_InputY2.IsReadOnly = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GeometryChoiceLabel.Content = "Выбрано: Sector";
            PaintViewModel.Geometry = GeometryEnum.Sector;
            TextBox_InputX2.IsReadOnly = true;
            TextBox_InputY2.IsReadOnly = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PaintViewModel.Geometry = GeometryEnum.Ellipse;
            GeometryChoiceLabel.Content = "Выбрано: Ellipse";
            TextBox_InputX2.IsReadOnly = true;
            TextBox_InputY2.IsReadOnly = true;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PaintViewModel.Geometry = GeometryEnum.Arc;
            GeometryChoiceLabel.Content = "Выбрано: Ark";
            TextBox_InputX2.IsReadOnly = false;
            TextBox_InputY2.IsReadOnly = false;
        }

        private Shape GetShape()
        {

            int height = PaintViewModel.Height;
            string dataString;
            if (PaintViewModel.EndSector > 180) 
            {
                dataString = $"M0,0 L0,-{height} A{height},{height} 0 0 1 {height * Math.Sin(180 * (Math.PI / 180d))}, {-height * Math.Cos(180 * (Math.PI / 180d))}";
                dataString += $" A{height},{height} 0 0 1 {height * Math.Sin(PaintViewModel.EndSector * (Math.PI / 180d))}, {-height * Math.Cos(PaintViewModel.EndSector * (Math.PI / 180d))} z";
            }
            else 
            {
                dataString = $"M0,0 L0,-{height} A{height},{height} 0 0 {(PaintViewModel.EndSector > 180 ? 0 : 1)} {height * Math.Sin(PaintViewModel.EndSector * (Math.PI / 180d))}, {-height * Math.Cos(PaintViewModel.EndSector * (Math.PI / 180d))} z";
            }
            
            return PaintViewModel.Geometry switch
            {
                GeometryEnum.Line => new Line 
                { 
                    X1=PaintViewModel.X1,
                    Y1=PaintViewModel.Y1,
                    X2=PaintViewModel.X2,
                    Y2=PaintViewModel.Y2,
                },
                GeometryEnum.Ellipse => new Ellipse()
                {

                    Width = PaintViewModel.Width,
                    Height = PaintViewModel.Height,
                    RenderTransform = new TranslateTransform() { X = PaintViewModel.X1, Y = PaintViewModel.Y1 },
                },
                GeometryEnum.Sector => new Path 
                {
                    
                    Data=Geometry.Parse(dataString), 
                    RenderTransform = new TranslateTransform() { X = PaintViewModel.X1, Y = PaintViewModel.Y1 } 
                },
                GeometryEnum.Arc => new Path
                {
                    Data = Geometry.Parse($"M0,0 A100,100 0 0 1 {PaintViewModel.X2},{PaintViewModel.Y2} "),
                    RenderTransform = new TranslateTransform() { X = PaintViewModel.X1, Y = PaintViewModel.Y1 }
                },
                GeometryEnum.Rectangle => new Rectangle
                {
                    Width = PaintViewModel.Width,
                    Height = PaintViewModel.Height,
                    RenderTransform = new TranslateTransform() { X = PaintViewModel.X1, Y = PaintViewModel.Y1 },
                },
                _ => throw new ArgumentException("Geomerty didn't choosed")
            };
        }
        private void PaintItButton_Click(object sender, RoutedEventArgs e)
        {
            var brush = new SolidColorBrush(ColorViewModel.CurrentColor);
            var geometry = GetShape();
            geometry.Stroke = brush;
            geometry.StrokeThickness = PaintViewModel.InkWidth;
            if (PaintViewModel.IsFilled)
            {
                geometry.Fill = brush;
            }
            MainPaintCanvas.Children.Add
            (
                geometry
            );
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void ChangeColorButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new RGBColorWidget(ColorViewModel);
            dialog.ShowDialog();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MainPaintCanvas.Children.Clear();
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainPaintCanvas.Width = e.NewSize.Width;
            MainPaintCanvas.Height = e.NewSize.Height;
        }

        private void TextPaintButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextInput.Text.Length > 0)
            {
                MainPaintCanvas.Children.Add
                (
                    new TextBlock
                    {
                        Text = TextInput.Text,
                        RenderTransform = new TranslateTransform() 
                        { 
                            X = PaintViewModel.X1, 
                            Y = PaintViewModel.Y1 
                        },
                        FontSize = PaintViewModel.InkWidth,
                        Foreground = new SolidColorBrush(ColorViewModel.CurrentColor)
                    }
                );
            }
        }
    }
}
