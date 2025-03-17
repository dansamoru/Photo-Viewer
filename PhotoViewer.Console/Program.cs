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
            Console.Write("Введите путь к папке с изображениями: ");
            string path = Console.ReadLine();

            try
            {
                navigator.ChangeFolder(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return;
            }

            List<ImageItem> images = navigator.GetImageFiles();
            Console.WriteLine("Найденные изображения:");
            foreach (var image in images)
            {
                Console.WriteLine($"Имя: {image.FileName} | Дата изменения: {image.ModifiedDate} | Путь: {image.FilePath}");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
