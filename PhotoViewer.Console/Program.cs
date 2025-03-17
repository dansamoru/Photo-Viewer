using System;
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
            navigator.ChangeFolder(path);

            var images = navigator.GetImageFiles();
            Console.WriteLine("Найденные изображения:");
            foreach (var file in images)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
