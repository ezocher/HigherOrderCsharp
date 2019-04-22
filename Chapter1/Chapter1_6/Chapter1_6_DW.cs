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

// Like the print_dir example in section 1.5
public class PrintAll : DirectoryWalker
{
    public override void FileOrDirectory(string path)
    {
        Console.WriteLine(path);
    }


    public static void Demo(string path)
    {
        Console.WriteLine("\n--------------- Chapter 1.6 Print_All ---------------");

        PrintAll pa = new PrintAll();
        pa.Dir_Walk(path);
        Console.WriteLine();
    }
}