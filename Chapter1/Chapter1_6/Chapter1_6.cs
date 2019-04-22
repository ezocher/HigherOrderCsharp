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

public class TotalSize : DirectoryWalker
{
    public override object File(string path)
    {
        if (PerlFileOp.Exists(path))
            return PerlFileOp.Size(path);
        else
        {
            Console.WriteLine("'{0}' - no such file or directory exists.", path);
            return 0;
        }
    }

    public override object Directory(string path, List<object> results)
    {
        long total = 0;
        foreach (long fileSize in results)
            total += fileSize;
        return total;
    }

    public static void Demo()
    {
        Console.WriteLine("\n--------------- Chapter 1.6 Total_Size ---------------");
        string[] paths = {
            @"c:\nosuchfileexists",
            @"C:\Temp\test.txt",
            Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)};

        TotalSize ts = new TotalSize();
        foreach (string path in paths)
        {
            Console.WriteLine("Size of {0} = {1:N0} bytes", path, ts.Dir_Walk(path));
        }
        Console.WriteLine();
    }
}