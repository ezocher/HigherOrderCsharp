/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */
 
using System;
using System.IO;

class Chapter1_4
{
    // The Perl implementation of total_size starting on page 12 has a bug that does not occur when implementing
    //  this in the straight-forward way in C#

    // total-size - Higher Order Perl pp. 8-15
    public static long Total_Size(string top)
    {
        long total = PerlFileOps.Size(top);  // -s

        if (PerlFileOps.IsFile(top)) return total;  // -f

        // There is not an equivalent to Perl's opendir/readir. The line below tries to read the files and directories 
        //  in directory "top" and will throw an exception if the directory with this name doesn't exist or has some 
        //  kind of access error
        FileSystemInfo[] files;
        try
        {
            files = (new DirectoryInfo(top)).GetFileSystemInfos();
        }
        catch (Exception e)
        {
            Console.WriteLine("Couldn't open directory {0}: {1}; skipping.", top, e.Message);
            return total;
        }

        foreach (FileSystemInfo file in files)
        {
            // System.IO doesn't return aliases like "." or ".." for any GetXXX calls
            //  so we don't need code to exclude them
            total += Total_Size(file.FullName);
        }

        // Don't need to "closedir" the directory, it is freed when out of scope
        return total;
    }




    public static void Demo_Total_Size()
    {
        Console.WriteLine("\n--------------- Chapter 1.4 Total_Size ---------------");
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
        Console.WriteLine();
    }

}
