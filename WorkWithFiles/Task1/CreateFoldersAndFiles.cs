
namespace Task1
{
    public static class CreateFoldersAndFiles
    {
        /// <summary>
        /// Check does the directory exist. If doesn't offers to the
        /// user options to choice
        /// </summary>
        /// <param name="pathToParentFolder"></param>
        public static void CheckDirectories(string pathToParentFolder)
        {
            if (!Directory.Exists(pathToParentFolder))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Каталог " + pathToParentFolder + " отсутствует");
                Console.WriteLine("\n" + "Создать для целей тестирования? (Выберите: 1 - Да, 0 - Нет");
                int.TryParse(Console.ReadLine(), out int answer);
                if (answer == 1)
                {
                    CreateSubFoldersAndFiles(pathToParentFolder);
                }
            }
        }

        /// <summary>
        /// Create SubFolders and Files inside ParentFolder for Demonstration
        /// </summary>
        /// <param name="pathToParentFolder"></param>
        private static void CreateSubFoldersAndFiles(string pathToParentFolder)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            DirectoryInfo dir = new DirectoryInfo(pathToParentFolder);

            // if directory on given path doesn't exist - create directory
            if (!dir.Exists)
            {
                dir.Create();
            }

            // if subdirectory and file on given path doesn't exist -
            // create 3 subdirectories with 1 .txt file inside
            if (Directory.GetDirectories(pathToParentFolder).Length == 0 &&
                    Directory.GetFiles(pathToParentFolder).Length == 0)
            {
                Console.WriteLine("Для целей тестирования ");
                for (int i = 0; i < 3; i++)
                {
                    string dirPath = pathToParentFolder + "Test" + i.ToString();
                    string filePath = dirPath + "\\" + i.ToString() + ".txt";
                    Directory.CreateDirectory(dirPath);

                    FileInfo fi = new FileInfo(filePath);
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine("Текстовый файл для тестирования " + Math.Pow(1000, i).ToString());
                    }

                    //File.Create(filePath);
                    Console.WriteLine("\n" + " Создан каталог: " + dirPath);
                    Console.WriteLine("В каталоге " + dirPath + " создан файл " + filePath);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}


