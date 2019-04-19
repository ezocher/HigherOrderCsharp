/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System.IO;


// Implementation of Perl file operators
class PerlFileOp
{
    // Implement an equivalent of the Perl -s file operator (Size of a file or directory)
    public static long Size(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
            return fi.Length;
        else
            return 0;
    }

    // Implement an equivalent of the Perl -f file operator (Is this a file)
    public static bool IsFile(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
            return !((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
        else
            return false;
    }

}
