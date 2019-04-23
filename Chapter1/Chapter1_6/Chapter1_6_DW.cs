/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;

// Duplicates functionality of print_dir on page 18
public class PrintAll : DirectoryWalker
{
    public override void FileOrDirectory(string path)
    {
        Console.WriteLine(path);
    }


    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 PrintAll ---------------");

        PrintAll pa = new PrintAll();
        pa.Dir_Walk(path);
        Console.WriteLine();
    }
}

// Duplicates functionality of printing out filenames with sizes on page 19
public class PrintWithSizes : DirectoryWalker
{
    private const string DirOrFileSizeMaxWrite = "99,999,999,999";       // Dirs or files up to almost 100 GB
    private static readonly string DirOrFileSizeFormat = "{0," + DirOrFileSizeMaxWrite.Length + ":N0} {1}";
    public override void FileOrDirectory(string path)
    {
        Console.WriteLine(DirOrFileSizeFormat, PerlFileOps.Size(path), path);
    }


    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 PrintWithSizes ---------------");

        PrintWithSizes pws = new PrintWithSizes();
        pws.Dir_Walk(path);
        Console.WriteLine();
    }
}

// Duplicates functionality of printing out filenames of dangling symbolic links on page 19 and pages 23-24
public class PrintDangles : DirectoryWalker
{
    public override void FileOrDirectory(string path)
    {
        string target;
        if (PerlFileOps.Link(path, out target) && !PerlFileOps.Exists(target))    // -l && -e
            Console.WriteLine("'{0}' => '{1}'", path, target);

    }


    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 PrintDangles ---------------");

        PrintDangles pd = new PrintDangles();
        pd.Dir_Walk(path);
        Console.WriteLine();
    }
}


// This one is not in the book, but I wanted to do it
// This class is initialized with a list of file extensions and then uses the DirectoryWalker to print all files with one of those extensions
//
// I am surprised that this only required 4 new lines of code and one new property
public class PrintFilesFilteredByExtension : DirectoryWalker
{
    private readonly HashSet<string> extensions = new HashSet<string>();

    PrintFilesFilteredByExtension(string[] extensionList)
    {
        foreach (string extension in extensionList)
            extensions.Add(extension.ToLower());
    }

    public override void FileOrDirectory(string path)
    {
        // Need to check that this is a file in case we have any wacky directory names like 'dirdir.jpg'
        if ( (PerlFileOps.IsFile(path)) && (extensions.Contains(Path.GetExtension(path).ToLower())) )
            Console.WriteLine(path);
    }


    public static void Demo(string path, string[] extensionList)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 PrintFilesFilteredByExtension for Photos ---------------");

        PrintFilesFilteredByExtension printPhotosFiles = new PrintFilesFilteredByExtension(extensionList);
        printPhotosFiles.Dir_Walk(path);
        Console.WriteLine();
    }
}
