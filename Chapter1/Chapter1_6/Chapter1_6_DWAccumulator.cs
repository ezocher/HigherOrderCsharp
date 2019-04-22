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

// Duplicates functionality of total-Size on page 15
public class TotalSize : DirectoryWalkerAccumulator
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

    public static void Demo(string[] pathList)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 TotalSize ---------------");

        TotalSize ts = new TotalSize();
        foreach (string path in pathList)
        {
            Console.WriteLine("Size of {0} = {1:N0} bytes", path, ts.Dir_Walk_Accumulator(path));
        }
        Console.WriteLine();
    }
}

// Duplicates functionality of printing out sizes of sub-directories on page 22
public class PrintSubdirSize : DirectoryWalkerAccumulator
{
    public override object File(string path)
    {
        return PerlFileOp.Size(path);
    }

    public override object Directory(string path, List<object> results)
    {
        long total = 0;
        foreach (long size in results)
            total += size;

        // Format specifier N0 displays numbers with comma seperators
        //   Use a width of 13 to accomodate sizes up to about 10 Gigabytes, e.g. 9,999,999,999
        Console.WriteLine("{0,13:N0} {1}", total, path);

        return total;
    }

    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 PrintSubdirSize ---------------");

        PrintSubdirSize pss = new PrintSubdirSize();
        pss.Dir_Walk_Accumulator(path);
        Console.WriteLine();
    }
}

// Duplicates functionality of sizehash on page 23
public class Sizehash : DirectoryWalkerAccumulator
{
    public override object File(string path)
    {
        List<Object> l = new List<Object>
        {
            Path.GetFileName(path),   // We don't need a sub short{} equivalent since we have Path.GetFileName()
            PerlFileOp.Size(path)
        };
        return l;
    }

    public override object Directory(string path, List<object> results)
    {
        Dictionary<string, Object> new_hash = new Dictionary<string, Object>();
        foreach (List<Object> o in results)
            new_hash.Add((string)o[0], o[1]);
        List<Object> result = new List<Object>
        {
            Path.GetFileName(path),
            new_hash
        };
        return result;
    }

    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 Sizehash ---------------");

        Sizehash sh = new Sizehash();
        List<Object> a = (List<Object>)sh.Dir_Walk_Accumulator(path);
        Console.WriteLine("Top directory = {0}", (string)a[0]);
        Console.WriteLine();
    }
}



// TODO: Plain files list from page 24
