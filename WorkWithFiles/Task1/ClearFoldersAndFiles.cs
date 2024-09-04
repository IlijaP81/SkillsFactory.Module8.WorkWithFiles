
namespace Task1
{
    static class ClearFoldersAndFiles
    {   
        static string initialFolder = Program.pathToParentFolder;
        static bool timeIsUp = false;
        static int filesDeleted  = 0;
        static long freedMemory = 0;
        public static void StartWork(string pathToParentFolder)
        {
            Console.WriteLine("Задайте интервал времени в минутах, по истечении которого " +
                              "при неиспользовании каталоги и файлы будут удалены: ");
            int.TryParse(Console.ReadLine(), out int timeWithNoActivities);

            ClearAllSubFoldersAndFiles(pathToParentFolder, timeWithNoActivities, 
                                       out int totalFilesDeleted, out long totalFreedMemory);
            Console.WriteLine("Удалено " + filesDeleted + " файлов" + "\n" +
                              "Освобождено " + totalFreedMemory + " байт");
        }

        /// <summary>
        /// Recursively clear all subfolders and files inside initial 
        /// folder which were not accesed within last X minutes 
        /// (defines by user)
        /// </summary>
        /// <param name="pathToParentFolder"></param>
        public static void ClearAllSubFoldersAndFiles(string pathToParentFolder, int timeWithNoActivities, 
                                                      out int totalFilesDeleted, out long totalFreedMemory) 
        {
            totalFilesDeleted = 0;
            totalFreedMemory = 0;
            
            try
            {
                DirectoryInfo dir = new DirectoryInfo(pathToParentFolder);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo f in files)
                {
                    if (f.LastAccessTime < DateTime.Now.Subtract(new TimeSpan(0, timeWithNoActivities, 0)))
                    {
                        
                        freedMemory += f.Length;
                        f.Delete(); timeIsUp = true;
                        filesDeleted++; totalFilesDeleted = filesDeleted;
                        totalFreedMemory = freedMemory;
                        Console.WriteLine("Удален файл " + f.FullName);
                    }
                }
                foreach (DirectoryInfo d in dirs)
                {
                    if (d.LastAccessTime < DateTime.Now.Subtract(new TimeSpan(0, timeWithNoActivities, 0)))
                    {
                    // deleting by recurcion
                    ClearAllSubFoldersAndFiles(d.ToString(), timeWithNoActivities, 
                                               out totalFilesDeleted, out totalFreedMemory);    
                    //d.Delete(true);
                    }
                }

                //clear parent folder
                if (dir.GetDirectories().Length == 0 && dir.GetFiles().Length == 0)
                {
                    //clear initial folder
                    if (dir.ToString() == initialFolder)
                    {
                        Console.WriteLine("\n" + "Удалить начальный каталог " + dir + "(Выберите: 1 - Да, 0 - Нет");
                        int.TryParse(Console.ReadLine(), out int answer);
                        if (answer == 1)
                        {
                            dir.Delete(); 
                            Console.WriteLine("Удален начальный каталог " + dir.FullName);
                        }
                    }
                    else
                    {
                        dir.Delete(); timeIsUp = true;
                        Console.WriteLine("Удален каталог " + dir.FullName);
                    }
                }
                if (!timeIsUp)
                {
                    Console.WriteLine($"Нет удаленных объектов, не вышло время ожидания {timeWithNoActivities} минут");
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Каталог по заданному пути отсутствует " + dnfe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("Произошла ошибка: " + uae.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
    }
}


