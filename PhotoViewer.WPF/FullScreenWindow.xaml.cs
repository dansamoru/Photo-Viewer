using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PhotoViewer.WPF
{
    public partial class FullScreenWindow : Window
    {
        public FullScreenWindow(string imagePath)
        {
            InitializeComponent();
            try
            {
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                FullScreenImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                this.Close();
            }
        }
    }
}
