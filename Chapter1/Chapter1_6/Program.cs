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

        TotalSize.Demo(pathList);   // This demo runs multiple paths since it doesn't do much for each one

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

        PrintAll.Demo(path);
        PrintWithSizes.Demo(path);
        PrintDangles.Demo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)); // UserProfile is a likely place to have many dangling links/shortcuts and will probably take a long time to run
        PrintSubdirSize.Demo(path);

        string[] photosFileExtensions = {
            ".jpg", ".jpeg", ".jpe", ".jif", ".jfif", ".jfi",   // JPEG
            ".jp2", ".j2k",  ".jpf", ".jpx", ".jpm",  ".mj2",   // JPEG 2000
            ".jxr", ".hdp",  ".wdp",                            // JPEG XR aka HD Photo
            ".heif", ".heic",                                   // HEIF

            // Raw image formats extensions from https://en.wikipedia.org/wiki/Raw_image_format
            ".3fr", ".ari", ".arw", ".bay", ".crw", ".cr2", ".cr3", ".cap", ".data", ".dcs", ".dcr", ".dng", ".drf", ".eip", ".erf", ".fff", ".gpr", ".iiq", ".k25",
            ".kdc", ".mdc", ".mef", ".mos", ".mrw", ".nef", ".nrw", ".obm", ".orf",  ".pef", ".ptx", ".pxn", ".r3d", ".raf", ".raw", ".rwl", ".rw2", ".rwz", ".sr2",
            ".srf", ".srw", ".tif", ".x3f"
        };

        PrintFilesFilteredByExtension.Demo(path, photosFileExtensions);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);
    }
}
