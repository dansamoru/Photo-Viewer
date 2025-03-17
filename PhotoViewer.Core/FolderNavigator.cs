namespace PhotoViewer.Core
{
    public class FolderNavigator
    {
        // Свойство для текущего пути
        public string CurrentPath { get; set; }

        // Делегат и событие для уведомления об изменении папки
        public delegate void FolderChangedEventHandler(string newPath);
        public event FolderChangedEventHandler FolderChanged;

        // Метод для получения списка файлов (например, изображений)
        public List<string> GetImageFiles()
        {
            if (string.IsNullOrEmpty(CurrentPath) || !Directory.Exists(CurrentPath))
                return new List<string>();

            // Фильтрация по расширениям файлов
            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            return Directory.GetFiles(CurrentPath)
                            .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                            .ToList();
        }

        // Метод для смены папки
        public void ChangeFolder(string newPath)
        {
            if (Directory.Exists(newPath))
            {
                CurrentPath = newPath;
                FolderChanged?.Invoke(newPath);
            }
        }
    }
}
