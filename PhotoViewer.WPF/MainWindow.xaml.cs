using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PhotoViewer.Core;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PhotoViewer.WPF
{
    public partial class MainWindow : Window
    {
        private FolderNavigator folderNavigator = new FolderNavigator();

        public MainWindow()
        {
            InitializeComponent();
            folderNavigator.FolderChanged += FolderNavigator_FolderChanged;
        }

        private void FolderNavigator_FolderChanged(string newPath)
        {
            var images = folderNavigator.GetImageFiles();
            ImagesListView.ItemsSource = images;
            PreviewImage.Source = null;
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Выберите папку с изображениями"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPathTextBox.Text = dialog.FileName;
                try
                {
                    folderNavigator.ChangeFolder(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии папки: " + ex.Message);
                }
            }
        }

        private void ImagesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ImagesListView.SelectedItem is ImageItem selectedItem)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedItem.FilePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    PreviewImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке предпросмотра: " + ex.Message);
                    PreviewImage.Source = null;
                }
            }
            else
            {
                PreviewImage.Source = null;
            }
        }

        private void ImagesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ImagesListView.SelectedItem is ImageItem selectedItem)
            {
                var images = ImagesListView.ItemsSource as List<ImageItem>;
                int currentIndex = images.IndexOf(selectedItem);
                FullScreenWindow fullScreenWindow = new FullScreenWindow(images, currentIndex);
                fullScreenWindow.Show();
            }
        }
    }
}
