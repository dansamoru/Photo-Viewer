using System;
using System.Collections.Generic;
using PhotoViewer.Core;

namespace PhotoViewer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            FolderNavigator navigator = new FolderNavigator();
            global::System.Console.Write("Введите путь к папке с изображениями: ");
            string path = global::System.Console.ReadLine();

            try
            {
                navigator.ChangeFolder(path);
            }
            catch (Exception ex)
            {
                global::System.Console.WriteLine("Ошибка: " + ex.Message);
                return;
            }

            List<ImageItem> images = navigator.GetImageFiles();
            global::System.Console.WriteLine("Найденные изображения:");
            foreach (var image in images)
            {
                global::System.Console.WriteLine($"Имя: {image.FileName} | Дата изменения: {image.ModifiedDate} | Путь: {image.FilePath}");
            }

            global::System.Console.WriteLine("Нажмите любую клавишу для выхода...");
            global::System.Console.ReadKey();
        }
    }
}
