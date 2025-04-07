using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoViewer.Core
{
    public class FolderNavigator
    {
        public string CurrentPath { get; set; }

        public delegate void FolderChangedEventHandler(string newPath);
        public event FolderChangedEventHandler FolderChanged;

        public List<ImageItem> GetImageFiles()
        {
            if (string.IsNullOrEmpty(CurrentPath) || !Directory.Exists(CurrentPath))
                return new List<ImageItem>();

            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            var files = Directory.GetFiles(CurrentPath)
                .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => new ImageItem
                {
                    FilePath = file,
                    FileName = Path.GetFileName(file),
                    ModifiedDate = File.GetLastWriteTime(file),
                    FileSize = FileInfoProvider.GetFileSize(file)
                })
                .ToList();

            return files;
        }

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
