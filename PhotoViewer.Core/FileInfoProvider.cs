using System;
using System.IO;

namespace PhotoViewer.Core
{
    public static class FileInfoProvider
    {
        // Метод для получения размера файла
        public static long GetFileSize(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo info = new FileInfo(filePath);
                return info.Length;
            }
            throw new FileNotFoundException("Файл не найден", filePath);
        }
    }
}
