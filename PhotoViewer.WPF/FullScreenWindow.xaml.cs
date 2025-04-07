using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PhotoViewer.Core;

namespace PhotoViewer.WPF
{
    public partial class FullScreenWindow : Window
    {
        private List<ImageItem> _images;
        private int _currentIndex;

        public FullScreenWindow(List<ImageItem> images, int currentIndex)
        {
            InitializeComponent();
            _images = images;
            _currentIndex = currentIndex;
            LoadImage();
            this.Loaded += (s, e) => this.Focus();
        }

        private void LoadImage()
        {
            var imageItem = _images[_currentIndex];
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imageItem.FilePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                FullScreenImage.Source = bitmap;

                long fileSize = FileInfoProvider.GetFileSize(imageItem.FilePath);
                FileInfoTextBlock.Text = $"{imageItem.FileName} | {fileSize / 1024} KB | {imageItem.ModifiedDate}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Right)
            {
                if (_currentIndex < _images.Count - 1)
                {
                    _currentIndex++;
                    LoadImage();
                }
            }
            else if (e.Key == Key.Left)
            {
                if (_currentIndex > 0)
                {
                    _currentIndex--;
                    LoadImage();
                }
            }
        }
    }
}
