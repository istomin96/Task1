using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using static Task1.Program;

namespace Task1
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            //Задание 1, 2, 3

            string path = @"D:\Папка с файлами\";

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            Delete(dirInfo, path);

            //Задание 4

            string path2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Students.dat";

            List<Student> studentsToWrite = new List<Student>
            {
                new Student { Name = "Софья", Group = "Группа 1", DateOfBirth = new DateTime(1996, 10, 12), AverageScore = 3.9M },
                new Student { Name = "Владимир", Group = "Группа 1", DateOfBirth = new DateTime(1988, 06, 14), AverageScore = 4.0M},
                new Student { Name = "Сергей", Group = "Группа 2", DateOfBirth = new DateTime(1998, 02, 24), AverageScore = 4.2M},
                new Student { Name = "Дмитрий", Group = "Группа 2", DateOfBirth = new DateTime(2000, 8, 26), AverageScore = 4.7M}
            };

            WriteStudents(studentsToWrite, path2);

            List<Student> studentsToRead = ReadStudents(path2);

            foreach (Student studentProp in studentsToRead)
            {
                Console.WriteLine(studentProp.Name + " " + studentProp.Group + " " + studentProp.DateOfBirth + " " + studentProp.AverageScore);
            }
        }

        //Задание 1. Метод удаления папки + Задание 3. Добавлен метод по размеру папки
        static void Delete(DirectoryInfo dir, string path)
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

        //Задание 2. Метод размера папки
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

        //Задание 4. Программа бинарного формата текста
        internal class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
            public decimal AverageScore { get; set; }

        }

        static void WriteStudents(List<Student> students, string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (File.Create(fileName));
            }
           
            using FileStream fs = new FileStream(fileName, FileMode.Create);
            using BinaryWriter bw = new BinaryWriter(fs);

            foreach (Student student in students)
            {
                bw.Write(student.Name);
                bw.Write(student.Group);
                bw.Write(student.DateOfBirth.ToBinary());
                bw.Write(student.AverageScore);
            }
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        static List<Student> ReadStudents(string fileName)
        {
            List<Student> result = new();
            using FileStream fs = new FileStream(fileName, FileMode.Open);
            using StreamReader sr = new StreamReader(fs);

            Console.WriteLine(sr.ReadToEnd());

            fs.Position = 0;

            BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                Student student = new Student();
                student.Name = br.ReadString();
                student.Group = br.ReadString();
                long dt = br.ReadInt64();
                student.DateOfBirth = DateTime.FromBinary(dt);
                student.AverageScore = br.ReadDecimal();

                result.Add(student);
            }

            fs.Close();

            return result;
        }
    }
}
