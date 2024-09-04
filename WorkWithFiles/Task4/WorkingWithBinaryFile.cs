using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task4
{
    public class WorkingWithBinaryFile
    {
        public static void StartWork(string path)
        {
            List<Student> students = ReadBinaryFile(path);
            WriteGroupedDataToFiles(students);
        }

        /// <summary>
        /// Creates folder on DeskTop and clear all existing files inside one
        /// </summary>
        private static string CreateFolder()
        {
            string newFolder = "Students";
            string folderOnDesktop = Path.Combine(
                                     Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                     newFolder);

            // if folder doesn't exist - create folder
            if (!Directory.Exists(folderOnDesktop))
            {
                Directory.CreateDirectory(folderOnDesktop);
            }

            else // delete all files in folder
            {
                DirectoryInfo dir = new DirectoryInfo(folderOnDesktop);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    f.Delete();
                }
            }
            return folderOnDesktop;
        }

        /// <summary>
        /// Reads content from binary file
        /// </summary>
        /// <param name="filePath"></param>
        private static List<Student> ReadBinaryFile(string path)
        {
            var file = Directory.GetFiles(path);
            string name;
            string group;
            double bd;
            decimal rank;
            List<Student> students = new List<Student>();

            string[] fileBytes = File.ReadAllLines(file[0]);
            using (BinaryReader reader = new BinaryReader(File.Open(file[0], FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    name = reader.ReadString();
                    group = reader.ReadString();
                    bd = reader.ReadDouble(); DateTime birthDate = DateTime.FromOADate(bd);
                    rank = reader.ReadDecimal();
                    students.Add(new Student(name, group, birthDate, rank));
                }
            }
            return students;
        }

        /// <summary>
        /// Groups and writes data to txt - file
        /// </summary>
        /// <param name="students"></param>
        private static void WriteGroupedDataToFiles(List<Student> students)
        {
            string folderOnDesktop = CreateFolder();
            string dataToWriteToFile = null;
            var result = students.GroupBy(student => student.Group).ToArray();
            if (result.Length > 0)
            {
                // cycle by Groups
                for (int i = 0; i < result.Length; i++)
                {
                    string fileName = "Группа" + result[i].Key.ToString() + ".txt";
                    string filePath = Path.Combine(folderOnDesktop, fileName);

                    IEnumerable<string> data = result[i].Select(x =>
                    $" Имя студента: {x.Name} " +
                    $" Дата рождения: {x.DateOfBirth} " +
                    $" Средний балл: {x.AverageRank} ");

                    foreach (string d in data)
                    {
                        dataToWriteToFile += d + "\n";
                    }
                    if (!File.Exists(filePath))
                    {
                        using (StreamWriter sw = File.CreateText(filePath))
                        {
                            sw.WriteLine(dataToWriteToFile); sw.Close();
                            dataToWriteToFile = null;
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + "Данные о студентах записаны в файл(ы) в каталоге " + folderOnDesktop);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Данные для записи в файл(ы) отсутствуют");
            }
        }
    }


    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AverageRank { get; set; }

        public Student(string name, string group, DateTime dateOfBirth, decimal averageRank)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
            AverageRank = averageRank;
        }
    }
}
