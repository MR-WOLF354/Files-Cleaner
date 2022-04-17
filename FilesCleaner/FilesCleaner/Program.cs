using System;
using System.IO;
using System.Linq;

namespace FilesCleaner
{
    class Program
    {

        static void Main(string[] args)
        {

            do
            {

                Console.WriteLine("Copy path name here to scan");
                string ScanPath = Console.ReadLine();

                if (File.Exists(ScanPath))
                {

                    DateTime CreationTime = File.GetCreationTime(ScanPath);
                    DateTime dt = File.GetLastAccessTime(ScanPath);
                    long length = new FileInfo(ScanPath).Length;

                    Console.WriteLine("File Info:");
                    Console.WriteLine("Creation Time: " + CreationTime);
                    Console.WriteLine("Last Access Time: " + dt);
                    Console.WriteLine("FileSize: {0} B ", length);

                }

                else if (Directory.Exists(ScanPath))
                {
                    Console.WriteLine("File Size: " + Directory.EnumerateFiles($"{ScanPath}", "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length) + " B");

                    //Folders Scanning

                    string[] ChildrenDirectory = Directory.GetDirectories(ScanPath);

                    Console.WriteLine("");

                    Console.WriteLine("Folders:");

                    for (long i = 0; i < ChildrenDirectory.Length; i++)
                    {
                        Console.WriteLine("");

                        string Child = ChildrenDirectory[i];

                        DateTime DirectoryCreationTime = Directory.GetCreationTime(Child);

                        //Folder Size Calculation

                        double sizeInBytes = Directory.EnumerateFiles($"{Child}", "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);

                        Console.WriteLine("Child " + i + " // ChildPath: " + Child + " // Size: " + sizeInBytes + " B " + " // CreationTime: " + DirectoryCreationTime);

                        Console.WriteLine("Child Childs:");

                        //Folders Child Childs Scanning

                        string[] ChildsPaths = Directory.GetDirectories(Child);

                        for (long j = 0; j < ChildsPaths.Length; j++)
                        {

                            string ChildOfChild = ChildsPaths[j];

                            double ChildFolderSize = Directory.EnumerateFiles($"{ChildOfChild}", "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length);

                            Console.WriteLine("Child Childs Folders: " + ChildsPaths[j] + " // CreationTime: " + DirectoryCreationTime + " // Size: " + ChildFolderSize + " B");

                        }


                        //Files Child Childs Scanning

                        string[] ChildsFiles = Directory.GetFiles(Child);

                        DateTime FileCreationTime = File.GetCreationTime(Child);

                        for (long j = 0; j < ChildsFiles.Length; j++)
                        {
                            long length = new FileInfo(ChildsFiles[j]).Length;

                            Console.WriteLine("Child Childs Files: " + ChildsFiles[j] + " // CreationTime: " + FileCreationTime + " // Size: " + length + " B");

                        }

                        Console.WriteLine("");

                    }

                    Console.WriteLine("");

                    //Files Scanning

                    Console.WriteLine("Files:");

                    string[] ChildrensFiles = Directory.GetFiles(ScanPath);

                    for (int j = 0; j < ChildrensFiles.Length; j++)
                    {

                        long FileSize = new FileInfo(ChildrensFiles[j]).Length;

                        Console.WriteLine("Child " + j + " // ChildPath: " + ChildrensFiles[j] + " // Size: " + FileSize + " B");

                    }

                    //Path deleting

                    Console.WriteLine("What path would you want to delete?");
                    string DeletingPath = Console.ReadLine();

                    string Odp;

                    if (File.Exists(DeletingPath))
                    {
                        Console.WriteLine("Are you Sure? (yes or no)");
                        Odp = Console.ReadLine();

                        if (Odp == "yes")
                        {

                            File.Delete(DeletingPath);

                        }

                        else
                        {

                            Console.WriteLine("Okay");

                        }


                    }

                    else if (Directory.Exists(DeletingPath))
                    {

                        Console.WriteLine("Are you Sure? (yes or no)");
                        Odp = Console.ReadLine();

                        if (Odp == "yes")
                        {

                            Directory.Delete(DeletingPath);
                            Console.WriteLine("Done");

                        }

                        else
                        {

                            Console.WriteLine("Okay");

                        }

                    }

                }
                else
                {

                    Console.WriteLine("Path doesn't exist, try again maybe");

                }

            } while (true);
        }

    }

}
