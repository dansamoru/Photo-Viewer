using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PhotoViewer.Core; // Для FolderNavigator и ImageItem
using Microsoft.WindowsAPICodePack.Dialogs; // Для CommonOpenFileDialog

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

        // Обработчик события смены папки – обновление списка изображений
        private void FolderNavigator_FolderChanged(string newPath)
        {
            var images = folderNavigator.GetImageFiles();
            ImagesListView.ItemsSource = images;
            // Очистить предпросмотр
            PreviewImage.Source = null;
        }

        // Обработчик нажатия кнопки "Открыть" для выбора папки с использованием CommonOpenFileDialog
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

        // Обработчик одиночного нажатия (SelectionChanged) для предпросмотра изображения
        private void ImagesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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

        // Обработчик двойного клика по элементу ListView – открытие изображения в полноэкранном режиме
        private void ImagesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ImagesListView.SelectedItem is ImageItem selectedItem)
            {
                FullScreenWindow fullScreenWindow = new FullScreenWindow(selectedItem.FilePath);
                fullScreenWindow.Show();
            }
        }
    }
}
