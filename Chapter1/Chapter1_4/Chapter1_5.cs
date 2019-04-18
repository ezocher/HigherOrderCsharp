/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;
using System.IO;

class Chapter1_5
{

    // total-size - Higher Order Perl pp. 8-15

    // Implement an equivalent of the Perl -s operator
    public static long Size(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
            return fi.Length;
        else
            return 0;
    }

    // Implement an equivalent of the Perl -f operator
    public static bool Is_File(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
            return !((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
        else
            return false;
    }

    public static void Demo_()
    {
        Console.WriteLine("\n--------------- Chapter 1.5 xxx ---------------");
        /*
        string[] paths = {
            @"c:\nosuchfileexists",
            @"C:\Temp\test.txt",
            Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)};

        foreach (string path in paths)
        {
            Console.WriteLine("Size of {0} = {1:N0} bytes", path, Total_Size(path));
        }
        */
        Console.WriteLine();
    }


}