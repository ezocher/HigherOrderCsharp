﻿/* 
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
    public override void FileOrDirectory(string path)
    {
        // Format specifier N0 displays numbers with comma seperators
        //   Use a width of 13 to accomodate sizes up to about 10 Gigabytes, e.g. 9,999,999,999
        Console.WriteLine("{0,13:N0} {1}", PerlFileOp.Size(path), path);
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
        if (PerlFileOp.Link(path, out target) && !PerlFileOp.Exists(target))    // -l && -e
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
// I am surprised that this only took 4 new lines of code
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
        if (extensions.Contains(Path.GetExtension(path).ToLower()))
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
