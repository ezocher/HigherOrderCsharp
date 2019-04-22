/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;
using System.IO;

public abstract class DirectoryWalker
{
    public void Dir_Walk(string top)
    {
        FileOrDirectory(top);

        if (PerlFileOp.IsDir(top)) // -d
        {
            FileSystemInfo[] filesAndDirs;
            try
            {
                filesAndDirs = (new DirectoryInfo(top)).GetFileSystemInfos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't open directory '{0}': {1} - skipping.", top, e.Message);
                return;
            }

            foreach (FileSystemInfo fileOrDir in filesAndDirs)
                Dir_Walk(fileOrDir.FullName);
        }
    }

    public abstract void FileOrDirectory(string path);
}
