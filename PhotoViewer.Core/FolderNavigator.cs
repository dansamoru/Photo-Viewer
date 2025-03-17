using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoViewer.Core
{
    public class FolderNavigator
    {
        // Текущий путь к папке
        public string CurrentPath { get; set; }

        // Делегат и событие для уведомления об изменении папки
        public delegate void FolderChangedEventHandler(string newPath);
        public event FolderChangedEventHandler FolderChanged;

        // Метод для получения списка изображений в текущей папке
        public List<ImageItem> GetImageFiles()
        {
            if (string.IsNullOrEmpty(CurrentPath) || !Directory.Exists(CurrentPath))
                return new List<ImageItem>();

            // Поддерживаемые расширения файлов
            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            var files = Directory.GetFiles(CurrentPath)
                .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => new ImageItem
                {
                    FilePath = file,
                    FileName = Path.GetFileName(file),
                    ModifiedDate = File.GetLastWriteTime(file)
                }).ToList();

            return files;
        }

        // Метод для смены папки
        public void ChangeFolder(string newPath)
        {
            if (Directory.Exists(newPath))
            {
                CurrentPath = newPath;
                FolderChanged?.Invoke(newPath);
            }
            else
            {
                throw new DirectoryNotFoundException($"Папка '{newPath}' не найдена.");
            }
        }
    }
}
