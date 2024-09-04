using Task1;
using Task2;
using Task4;

class Program
{
    public const string pathToParentFolder = @"C:\Temp\";
    static void Main(string[] args)
    {
        DoOperations();
    }

    private static void DoOperations()
    {
        // Task3
        CreateFoldersAndFiles.CheckDirectories(pathToParentFolder);
        CalculateFolderSize.StartWork(pathToParentFolder);
        ClearFoldersAndFiles.StartWork(pathToParentFolder);
        CalculateFolderSize.StartWork(pathToParentFolder);

        // Task4
        string relativePath = @"..\..\students.dat";
        FileInfo fileInfo = new FileInfo(relativePath);
        string path = fileInfo.DirectoryName.Replace("bin", "File");

        //string path = @"C:\CSharp\SF\Module8\WorkWithFiles\WorkWithFiles\Task1\File\";
        WorkingWithBinaryFile.StartWork(path);
    }
}