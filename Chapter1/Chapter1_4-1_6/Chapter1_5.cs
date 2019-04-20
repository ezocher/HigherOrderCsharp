/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;
using System.Collections;
using System.IO;

class Chapter1_5
{
    public delegate void ItemFunc(string path);

    // dir-walk-simple - Higher Order Perl pp. 17-20
    public static void Dir_Walk_Simple(string top, ItemFunc code)
    {
        code(top);

        if (PerlFileOp.IsDir(top)) // -d
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
                Console.WriteLine("Couldn't open directory {0}: {1}; skipping.", top, e.Message);
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

    public delegate Object FileFunc(string filePath);
    public delegate Object DirFunc(string dirPath, ArrayList results);

    // dir-walk-cb - Higher Order Perl pp. 21-22
    public static Object Dir_Walk_CB(string top, FileFunc fileFunc, DirFunc dirFunc)
    {
        if (PerlFileOp.IsDir(top)) // -d
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
                Console.WriteLine("Couldn't open directory {0}: {1}; skipping.", top, e.Message);
                return null;
            }

            ArrayList results = new ArrayList();
            foreach (FileSystemInfo file in filesAndDirs)
            {
                // System.IO doesn't return aliases like "." or ".." for any GetXXX calls
                //  so we don't need code to exclude them
                results.Add(Dir_Walk_CB(file.FullName, fileFunc, dirFunc));
            }
            return dirFunc(top, results);
        }
        else
        {
            return fileFunc(top);
        }
    }

    public static Object File_Size(string path)
    {
        return PerlFileOp.Size(path);
    }

    public static Object Dir_Size(string dir, ArrayList results)
    {
        long total = 0;
        foreach (long n in results)
            total += n;
        return total;
    }

    public static Object Dir_Size_DU(string dir, ArrayList results)
    {
        long total = 0;
        foreach (long n in results)
            total += n;
        Console.WriteLine("{0,13:N0} {1}", total, dir);
        return total;
    }


    // dir-walk-sizehash - Higher Order Perl pp. 23-
    public static Object File(string path)
    {
        Hashtable h = new Hashtable();
        h.Add(Path.GetFileName(path), PerlFileOp.Size(path));      // We don't need a sub short{} equivalent since we have Path.GetFileName()
        return h;
    }


    public static void Demo_Dir_Walk_Simple()
    {
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple ---------------");
        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
        // Other directories you might want to try
        //  Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //  Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        Dir_Walk_Simple(path, Print_Dir);
        Console.WriteLine();

        // dir-walk-simple called with lambdas - Higher Order Perl p. 19
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple w/ lambda ---------------");
        // Same as Print_Dir, but using an expression lambda
        Dir_Walk_Simple(path, (x) => Console.WriteLine(x));
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple w/ sizes ---------------");
        // Format specifier N0 displays numbers with comma seperators (different than in the book, but easier to read)
        //   Use a width of 13 to accomodate sizes up to about 10 Gigabytes, e.g. 9,999,999,999
        Dir_Walk_Simple(path, (x) => Console.WriteLine("{0,13:N0} {1}", PerlFileOp.Size(x), x));
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple that displays Broken Links/Shortcuts ---------------");
        path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        Dir_Walk_Simple(path, (x) =>
        {
            string target;
            if (PerlFileOp.Link(x, out target) && !PerlFileOp.Exists(target))    // -l && -e
                Console.WriteLine("\"{0}\" => \"{1}\"", x, target);
        });
        Console.WriteLine();

        // dir-walk-simple used with an accumulator in the lambda - Higher Order Perl p. 20
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple w/ total size ---------------");
        path = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
        long total = 0;
        Dir_Walk_Simple(path, (x) => total += PerlFileOp.Size(x));
        Console.WriteLine("Total size of \"{0}\" is {1:N0}", path, total);
        Console.WriteLine();
    }

    public static void Demo_Dir_Walk_CB()
    {
        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB ---------------");
        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);

        long total = (long)Dir_Walk_CB(path, File_Size, Dir_Size);
        Console.WriteLine("Total size of \"{0}\" is {1:N0}", path, total);
        Console.WriteLine();

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_CB with usage per subdirectory (like du) ---------------");
        total = (long)Dir_Walk_CB(path, File_Size, Dir_Size_DU);
        Console.WriteLine("Total size of \"{0}\" is {1:N0}", path, total);
        Console.WriteLine();



    }
}