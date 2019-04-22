/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;

class Program
{
    static void Main(string[] args)
    {
        TotalSize.Demo();

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        // Other directories you might want to try
        //      Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
        //      Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //      Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        PrintAll.Demo(path);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);
    }
}
