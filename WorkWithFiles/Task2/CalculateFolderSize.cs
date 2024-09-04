using System;
using System.IO;

namespace Task2
{
    public static class CalculateFolderSize
    {
        static long folderSize;
        static public void StartWork(string pathToParentFolder)
        {
            folderSize = 0;
            DoFolderSizeCalculation(pathToParentFolder);
            Console.WriteLine($"Размер каталога {pathToParentFolder} " + folderSize + " байт");
        }

        /// <summary>
        /// Recursively calculates size of all subfolders and files inside initial folder 
        /// </summary>
        /// <param name="pathToParentFolder"></param>
        static public void DoFolderSizeCalculation(string pathToParentFolder)
        {
            try
            {
                DirectoryInfo path = new DirectoryInfo(pathToParentFolder);
                FileInfo[] files = path.GetFiles();
                foreach (FileInfo f in files)
                {
                    folderSize += f.Length;
                }

                DirectoryInfo[] dirs = path.GetDirectories();
                foreach (DirectoryInfo d in dirs) 
                {
                    // recursive call
                    DoFolderSizeCalculation(d.ToString());                   
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                
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
