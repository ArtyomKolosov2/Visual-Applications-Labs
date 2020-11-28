using Lab_Work_11.ViewModels;
using Lab_Work_9.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public RGBViewModel viewModel { get; set; } = new ();
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new RGBColorWidget(viewModel);
            dialog.ShowDialog();
            MainCanvas.DefaultDrawingAttributes.Color = viewModel.CurrentColor;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainCanvas.DefaultDrawingAttributes.Width = e.NewValue;
            MainCanvas.DefaultDrawingAttributes.Height = e.NewValue;
            SizeText.Text = $"Size: {Math.Round(e.NewValue)}px";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new() 
            { 
                Filter = "Png Files (*.png)|*.png" }
            ;
            if (saveFileDialog.ShowDialog() == true)
            {
                string imgPath = saveFileDialog.FileName;
                MemoryStream ms = new MemoryStream();
                FileStream fs = new FileStream(imgPath, FileMode.Create);
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)MainCanvas.Width, (int)MainCanvas.Height, 96, 96, PixelFormats.Default);
                rtb.Render(MainCanvas);
                PngBitmapEncoder gifEnc = new PngBitmapEncoder();
                gifEnc.Frames.Add(BitmapFrame.Create(rtb));
                gifEnc.Save(fs);
                fs.Close();
            }
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainCanvas.Width = e.NewSize.Width;
            MainCanvas.Height = e.NewSize.Height;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainCanvas.Strokes.Clear();
        }
    }
}
