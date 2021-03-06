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

class Chapter1_5
{
    public delegate void ItemFunc(string path);

    // dir-walk-simple - Higher Order Perl pp. 17-20
    public static void Dir_Walk_Simple(string top, ItemFunc code)
    {
        code(top);

        if (PerlFileOps.IsDir(top)) // -d
        {
            // There is not an equivalent to Perl's opendir/readir. The line below tries to read the files and directories 
            //  in directory "top" and will throw an exception if the directory with this name doesn't exist or has some 
            //  kind of access error
            FileSystemInfo[] filesAndDirs;
            try
            {
                filesAndDirs = (new DirectoryInfo(top)).GetFileSystemInfos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't open directory {0}: {1} - skipping.", top, e.Message);
                return;
            }

            foreach (FileSystemInfo file in filesAndDirs)
            {
                // System.IO doesn't return aliases like "." or ".." for any GetXXX calls
                //  so we don't need code to exclude them
                Dir_Walk_Simple(file.FullName, code);
            }
        }
    }

    public static void Print_Dir(string path)
    {
        Console.WriteLine(path);
    }

    public delegate object FileFunc(string filePath);
    public delegate object DirFunc(string dirPath, List<object> results);

    // dir-walk-cb - Higher Order Perl pp. 21-22
    public static object Dir_Walk_CB(string top, FileFunc fileFunc, DirFunc dirFunc)
    {
        if (PerlFileOps.IsDir(top)) // -d
        {
            // There is not an equivalent to Perl's opendir/readir. The line below tries to read the files and directories 
            //  in directory "top" and will throw an exception if the directory with this name doesn't exist or has some 
            //  kind of access error
            FileSystemInfo[] filesAndDirs;
            try
            {
                filesAndDirs = (new DirectoryInfo(top)).GetFileSystemInfos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't open directory {0}: {1} - skipping.", top, e.Message);
                return null;
            }

            // Using List<object> vs. ArrayList, references: 
            //      https://docs.microsoft.com/en-us/dotnet/api/system.collections.arraylist?view=netframework-4.8#remarks
            //      https://github.com/dotnet/platform-compat/blob/master/docs/DE0006.md
            List<object> results = new List<object>();       
            foreach (FileSystemInfo file in filesAndDirs)
            {
                // System.IO doesn't return aliases like "." or ".." for any GetXXX calls
                //  so we don't need code to exclude them
                object r = Dir_Walk_CB(file.FullName, fileFunc, dirFunc);
                if (r != null)
                    results.Add(r);
            }
            return dirFunc(top, results);
        }
        else
        {
            return fileFunc(top);
        }
    }

    public static object File_Size(string path)
    {
        return PerlFileOps.Size(path);
    }

    public static object Dir_Size(string dir, List<object> results)
    {
        long total = 0;
        foreach (long n in results)
            total += n;
        return total;
    }

    private const string DirOrFileSizeMaxWrite = "99,999,999,999";       // Dirs or files up to almost 100 GB
    private static readonly string DirOrFileSizeFormat = "{0," + DirOrFileSizeMaxWrite.Length + ":N0} {1}";
    public static object Dir_Size_DU(string dir, List<object> results)
    {
        long total = 0;
        foreach (long n in results)
            total += n;
        Console.WriteLine(DirOrFileSizeFormat, total, dir);
        return total;
    }

    // dir-walk-sizehash - Higher Order Perl pp. 23-24
    public static object File(string file)
    {
        List<object> al = new List<object>
        {
            Path.GetFileName(file),   // We don't need a sub short{} equivalent since we have Path.GetFileName()
            PerlFileOps.Size(file)
        };
        return al;
    }

    public static object Dir(string dir, List<object> subdirs)
    {
        // Changed from Hashtable to Dictionary<string, object> because of Microsoft recommendation:
        //      https://github.com/dotnet/platform-compat/blob/master/docs/DE0006.md
        Dictionary<string, object> new_hash = new Dictionary<string, object>();
        foreach (List<object> o in subdirs)
            new_hash.Add((string)o[0], o[1]);
        List<object> result = new List<object>();
        result.Add(Path.GetFileName(dir));
        result.Add(new_hash);
        return result;
    }

    // for the print_filename exmple we need to create two methods since the FileFunc and DirFunc have different numbers of parameters
    public static object Print_Filename(string name)
    {
        Console.WriteLine(name);
        return null;
    }

    public static object Print_Dirname(string name, List<object> empty)
    {
        return Print_Filename(name);
    }

    // dangles example
    public static object Dangles(string file)
    {
        if (PerlFileOps.Link(file, out string target) && !PerlFileOps.Exists(target))    // -l && -e
            Console.WriteLine("'{0}' => '{1}'", file, target);
        return null;
    }

    // dir-walk-cb-def - Higher Order Perl p. 24
    public static object Dir_Walk_CB_Def(string top, FileFunc fileFunc, DirFunc dirFunc)
    {
        if (PerlFileOps.IsDir(top)) // -d
        {
            // There is not an equivalent to Perl's opendir/readir. The line below tries to read the files and directories 
            //  in directory "top" and will throw an exception if the directory with this name doesn't exist or has some 
            //  kind of access error
            FileSystemInfo[] filesAndDirs;
            try
            {
                filesAndDirs = (new DirectoryInfo(top)).GetFileSystemInfos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't open directory {0}: {1} - skipping.", top, e.Message);
                return null;
            }

            List<object> results = new List<object>();
            foreach (FileSystemInfo file in filesAndDirs)
            {
                // System.IO doesn't return aliases like "." or ".." for any GetXXX calls
                //  so we don't need code to exclude them
                object r = Dir_Walk_CB_Def(file.FullName, fileFunc, dirFunc);
                if (r != null)
                    results.Add(r);
            }
            return (dirFunc != null) ? dirFunc(top, results) : null;
        }
        else
        {
            return (fileFunc != null) ? fileFunc(top) : null;
        }
    }

    public static void Demo_Dir_Walk_Simple(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple ---------------");
        Dir_Walk_Simple(path, Print_Dir);
        Console.WriteLine();

        // dir-walk-simple called with lambdas - Higher Order Perl p. 19
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple w/ lambda ---------------");
        // Same as Print_Dir, but using an expression lambda
        Dir_Walk_Simple(path, (x) => Console.WriteLine(x));
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple w/ sizes ---------------");
        Dir_Walk_Simple(path, (x) => Console.WriteLine(DirOrFileSizeFormat, PerlFileOps.Size(x), x));
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple that displays Broken Links/Shortcuts ---------------");
        Dir_Walk_Simple(path, (x) =>
        {
            if (PerlFileOps.Link(x, out string target) && !PerlFileOps.Exists(target))    // -l && -e
                Console.WriteLine("'{0}' => '{1}'", x, target);
        });
        Console.WriteLine();

        // dir-walk-simple used with an accumulator in the lambda - Higher Order Perl p. 20
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple w/ total size ---------------");
        long total = 0;
        Dir_Walk_Simple(path, (x) => total += PerlFileOps.Size(x));
        Console.WriteLine("Total size of '{0}' is {1:N0}", path, total);
        Console.WriteLine();
    }

    public static void Demo_Dir_Walk_CB(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB ---------------");
        long total = (long)Dir_Walk_CB(path, File_Size, Dir_Size);
        Console.WriteLine("Total size of '{0}' is {1:N0}", path, total);
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB with usage per subdirectory (like du) ---------------");
        total = (long)Dir_Walk_CB(path, File_Size, Dir_Size_DU);
        Console.WriteLine("Total size of '{0}' is {1:N0}", path, total);
        Console.WriteLine();
    }

    public static void Demo_Dir_Walk_Sizehash(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Sizehash  ---------------");
        List<object> a = (List<object>)Dir_Walk_CB(path, File, Dir);
        Console.WriteLine("Top directory = {0}", (string)a[0]);
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB with print_filename  ---------------");
        // for the print_filename exmple we need to create two methods since the FileFunc and DirFunc have different numbers of parameters
        Dir_Walk_CB(path, Print_Filename, Print_Dirname);
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB with dangles  ---------------");
        // for the Dangles example we can't pass an empty method for the DirFunc, so we put a do-nothing method in a lambda
        Dir_Walk_CB(path, Dangles, (x, y) => { return null; });
        Console.WriteLine();
    }

    public static void Demo_Dir_Walk_CB_Def(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB_Def with dangles ---------------");
        // We can't omit the third parameter to Dir_Walk_CB_Def, but now we can pass in null instead of a method
        Dir_Walk_CB_Def(path, Dangles, null);
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB_Def, return list of files ---------------");
        List<object> allPlainFiles = (List<object>)Dir_Walk_CB_Def(path, (x) => { return x; },
            (x, result) => 
            {
                // result is a List<object> that contains a string (with the filename) for each file in the directory and a 
                //      List<object> of strings of filenames for each directory in the directory
                // We need to explicitly flatten these into a single List<object> of strings to pass up to our parent directory
                //      since C# doesn't have Perl's parameter stack manipulation and implicit array insertion
                int length = result.Count;
                int i = 0;
                for (int count = 1; count <= length; count++)
                {
                    object o = result[i];
                    if (o is List<object>)
                    {
                        // Insert list of strings into main list
                        result.RemoveAt(i);
                        result.InsertRange(i, ((List<object>)o));
                        i += ((List<object>)o).Count;
                    }
                    else
                    {
                        i++;
                    }
                }
                return result;
            });
        foreach (string file in allPlainFiles)
            Console.WriteLine(file);
        Console.WriteLine();
    }
}