using System.Windows;

namespace PhotoViewer.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Здесь можно загрузить список изображений, используя логику из PhotoViewer.Core
        }

        private void ImagesListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Получаем выбранный элемент
            var selectedItem = ImagesListView.SelectedItem as ImageItem; // ImageItem – модель для изображения
            if (selectedItem != null)
            {
                // Создаем новое окно для показа изображения в полном экране
                FullScreenWindow fullScreenWindow = new FullScreenWindow(selectedItem.FilePath);
                fullScreenWindow.Show();
            }
        }
    }
}
