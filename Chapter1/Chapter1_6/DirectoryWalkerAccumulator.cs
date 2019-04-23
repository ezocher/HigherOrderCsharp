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


public abstract class DirectoryWalkerAccumulator
{
    public object Dir_Walk_Accumulator(string top)
    {
        if (PerlFileOps.IsDir(top)) // -d
        {
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

            List<object> results = new List<object>();
            foreach (FileSystemInfo file in filesAndDirs)
            {
                object r = Dir_Walk_Accumulator(file.FullName);
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

    public abstract object File(string path);

    public abstract object Directory(string path, List<object> results);
}