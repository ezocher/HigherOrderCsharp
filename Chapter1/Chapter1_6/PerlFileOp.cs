/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */
using System;
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
        return File.Exists(path);
    }

    // Implement an equivalent of the Perl -d file operator (Is this a directory)
    public static bool IsDir(string path)
    {
        return Directory.Exists(path);
    }

    // Implement an equivalent of the Perl -e file operator (File or directory Exists)
    public static bool Exists(string path)
    {
        return (File.Exists(path) || Directory.Exists(path));
    }

    // Shortcuts/Links reference:
    //      https://code.msdn.microsoft.com/windowsdesktop/Identifying-and-Resolving-ca0dfce8
    //
    // The code at the link above no longer works
    //  The GetShell32NameSpaceFolder method below is necessary on Windows 8 and up
    //      See: https://social.msdn.microsoft.com/Forums/vstudio/en-US/b25e2b8f-141a-4a1c-a73c-1cb92f953b2b/instantiate-shell32shell-object-in-windows-8?forum=clr
    private static Shell32.Folder GetShell32NameSpaceFolder(Object folder)
    {
        Type shellAppType = Type.GetTypeFromProgID("Shell.Application");

        object shell = Activator.CreateInstance(shellAppType);
        return (Shell32.Folder)shellAppType.InvokeMember("NameSpace",
          System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { folder });
    }

    // Implement an equivalent of the Perl -l file operator (Resolve symbolic Link, this is a Link/Shortcut on Windows)
    // Returns false if path is not a Link/Shortcut file
    // Returns true and full path (via out target) of resolved Link/Shortcut if path is a link
    //
    // In C# we need to return the target of the link with an out string parameter
    //
    public static bool Link(string path, out string target)
    {
        const string ShortcutExtension = ".lnk";

        target = path; // Return original path if we don't find a link

        FileInfo fi = new FileInfo(path);
        if (fi.Exists && (fi.Extension.ToLower() == ShortcutExtension))
        {
            string directory = Path.GetDirectoryName(path);
            string file = Path.GetFileName(path);

            // Shell32.Shell shell = new Shell32.Shell();               // Doesn't work on Windows 8 and later
            // Shell32.Folder folder = shell.NameSpace(directory);
            Shell32.Folder folder = GetShell32NameSpaceFolder(directory);
            Shell32.FolderItem folderItem = folder.ParseName(file);


            if ((folderItem != null) && folderItem.IsLink)
            {
                try
                {
                    Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                    target = link.Path;
                    return true;
                }
                catch     // Silently catch any Access is denied excepions or other exceptions
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
