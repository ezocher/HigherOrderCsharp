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

public abstract class DirectoryWalker
{
    public Object Dir_Walk(string top)
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
                Console.WriteLine("Couldn't open directory '{0}': {1} - skipping.", top, e.Message);
                return null;
            }

            List<Object> results = new List<Object>();
            foreach (FileSystemInfo file in filesAndDirs)
            {
                // System.IO doesn't return aliases like "." or ".." for any GetXXX calls
                //  so we don't need code to exclude them
                Object r = Dir_Walk(file.FullName);
                if (r != null)
                    results.Add(r);
            }
            return Directory(top, results);
        }
        else
        {
            return File(top);
        }
    }

    public abstract Object File(string path);

    public abstract Object Directory(string path, List<Object> results);

}