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
        string[] pathList = {
            @"c:\nosuchfileexists",
            @"C:\Temp\test.txt",
            Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)};

        //TotalSize.Demo(pathList);   // This demo runs multiple paths since it doesn't do much for each one

        /*
         * The Demo()s below all take one path - some of them are long running or produce a lot of output depending on your numbers of files
         * 
         */

        string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        // Other directories you might want to try
        //      Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //      Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
        //      Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //      Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //      @"C:\Users\"

        //PrintAll.Demo(path);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);
    }
}
