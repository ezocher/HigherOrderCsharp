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
    public delegate void ItemFunc(string path);

    // dir-walk-simple - Higher Order Perl pp. 17-19
    public static void Dir_Walk_Simple(string top, ItemFunc code)
    {
        code(top);

        if (PerlFileOp.IsDir(top)) // -d
        {
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
                // Console.WriteLine("Couldn't open directory {0}: {1}; skipping.", top, e.Message);
                return;
            }

            foreach (FileSystemInfo file in files)
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

        Console.WriteLine("\n--------------- Chapter 1.5 Dir_Walk_Simple that displays Links/Shortcuts ---------------");
        // The version in the book displays dangling symbolic links
        //  This version instead displays all Shorcuts and the files/directories they point to and indicates whether they are good or broken/dangling Shortcuts
        path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        Dir_Walk_Simple(path, (x) =>
        {
            string target;
            if (PerlFileOp.Link(x, out target))     // -l
                Console.WriteLine("{2} Link: \"{0}\" => \"{1}\"", x, target, PerlFileOp.Exists(target) ? "Good" : "**BROKEN**");
        });
        Console.WriteLine();
    }


}