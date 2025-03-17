using System;
using System.Windows;
using PhotoViewer.Core; // Для FolderNavigator и ImageItem
using WinForms = System.Windows.Forms; // Для диалога выбора папки

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
        }

        // Обработчик нажатия кнопки "Открыть" для выбора папки
        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new WinForms.FolderBrowserDialog())
            {
                WinForms.DialogResult result = dialog.ShowDialog();
                if (result == WinForms.DialogResult.OK)
                {
                    FolderPathTextBox.Text = dialog.SelectedPath;
                    try
                    {
                        folderNavigator.ChangeFolder(dialog.SelectedPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при открытии папки: " + ex.Message);
                    }
                }
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
