﻿/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

// .csproj NOTE: 
//      This Project targets .NET Framework 4.7.2 because Shell32 COM interop (needed for file system shortcuts/links)
//      is not yet available in .NET Core 2.1 

using System;

class Program
{
    static void Main(string[] args)
    {
        Chapter1_4.Demo_Total_Size();

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        // Other directories you might want to try
        //      Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
        //      Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //      Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        Chapter1_5.Demo_Dir_Walk_Simple(path);
        Chapter1_5.Demo_Dir_Walk_CB(path);
        Chapter1_5.Demo_Dir_Walk_Sizehash(path);
        Chapter1_5.Demo_Dir_Walk_CB_Def(path);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);
    }
}


