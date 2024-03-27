using System.Drawing;
using System.Numerics;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"D:\Папка с файлами\");

            Delete(dirInfo);

        }

        //Метод удаления папки + добавлен метод по размеру папки
        static void Delete(DirectoryInfo dir)
        {
            if (dir.Exists)
            {
                Console.WriteLine($"Исходный размер папки: {DirSize(dir)}");

                try
                {
                    dir.Delete(true);
                    Console.WriteLine("Каталог удален");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка удаления {e.Message}");
                }

                Console.WriteLine($"Текущий размер папки: {DirSize(dir)}");
            }

            else
            {
                Console.WriteLine($"Папка отсутствует");
            }
        }

        //Метод размера папки
        static long DirSize(DirectoryInfo dir)
        {
            long size = default;

            if (dir.Exists)
            {
                FileInfo[] fis = dir.GetFiles();

                foreach (FileInfo file in fis)
                {
                    size += file.Length;
                }

                DirectoryInfo[] dis = dir.GetDirectories();

                foreach (DirectoryInfo di in dis)
                {
                    size += DirSize(di);
                }
            }

            return size;
        }


        static void Students()
        {
            string Name;
            string Group;
            DateTime DateOfBirth;
            decimal Dec;

        }
    }
}
