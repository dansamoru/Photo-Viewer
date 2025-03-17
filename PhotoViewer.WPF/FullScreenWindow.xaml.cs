using System;
using System.Windows;
using System.Windows.Input;
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
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                FullScreenImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                this.Close();
            }
            // Устанавливаем фокус, чтобы окно получало события клавиатуры
            this.Loaded += (s, e) => this.Focus();
        }

        // Обработчик клавиш – выход из полноэкранного режима при нажатии ESC
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
