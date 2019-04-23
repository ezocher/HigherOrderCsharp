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
        if (PerlFileOps.Exists(path))
            return PerlFileOps.Size(path);
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
        return PerlFileOps.Size(path);
    }

    private const string DirOrFileSizeMaxWrite = "99,999,999,999";       // Dirs or files up to almost 100 GB
    private static readonly string DirOrFileSizeFormat = "{0," + DirOrFileSizeMaxWrite.Length + ":N0} {1}";
    public override object Directory(string path, List<object> results)
    {
        long total = 0;
        foreach (long size in results)
            total += size;

        Console.WriteLine(DirOrFileSizeFormat, total, path);

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
        List<object> l = new List<object>
        {
            Path.GetFileName(path),   // We don't need a sub short{} equivalent since we have Path.GetFileName()
            PerlFileOps.Size(path)
        };
        return l;
    }

    public override object Directory(string path, List<object> results)
    {
        Dictionary<string, object> new_hash = new Dictionary<string, object>();
        foreach (List<object> o in results)
            new_hash.Add((string)o[0], o[1]);
        List<object> result = new List<object>
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
        List<object> a = (List<object>)sh.Dir_Walk_Accumulator(path);
        Console.WriteLine("Top directory = {0}", (string)a[0]);
        Console.WriteLine();
    }
}

// Duplicates functionality of all_plain_files on page 24
public class ListOfAllPlainFiles : DirectoryWalkerAccumulator
{
    public override object File(string path)
    {
        return path;
    }

    public override object Directory(string path, List<object> results)
    {
        // result is a List<object> that contains a string (with the filename) for each file in the directory and a 
        //      List<object> of strings of filenames for each directory in the directory
        // We need to explicitly flatten these into a single List<object> of strings to pass up to our parent directory
        //      since C# doesn't have Perl's parameter stack manipulation and implicit array insertion
        int length = results.Count;
        int i = 0;
        for (int count = 1; count <= length; count++)
        {
            object o = results[i];
            if (o is List<object>)
            {
                // Insert list of strings into main list
                results.RemoveAt(i);
                results.InsertRange(i, ((List<object>)o));
                i += ((List<object>)o).Count;
            }
            else
            {
                i++;
            }
        }
        return results;
    }

    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 ListOfAllPlainFIles ---------------");

        ListOfAllPlainFiles lapf = new ListOfAllPlainFiles();
        List<object> allPlainFiles = (List<object>)lapf.Dir_Walk_Accumulator(path);
        Console.WriteLine("Number of files = {0}", allPlainFiles.Count);
        foreach (string file in allPlainFiles)
            Console.WriteLine(file);
        Console.WriteLine();
    }
}

