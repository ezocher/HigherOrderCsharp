/*
 * https://github.com/ezocher/HigherOrderCsharp
 *
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 *
 */

using System;
using AngleSharp.Html.Dom; // NuGet package
using AngleSharp.Html.Parser;

public class Chapter1_7
{
    // first parse - Higher Order Perl pp. 26-28
    public static IHtmlDocument FirstParse()
    {
        var parser = new HtmlParser();
        IHtmlDocument tree = parser.ParseDocument(HTMLSamples.EncloseInHeaderFooter(HTMLSamples.Simple_Page26));
        return tree;
    }

    public static void Demo_First_Parse()
    {
        Console.WriteLine("\n--------------- Chapter 1.7 First parse ---------------\n");

        var tree = FirstParse();

        Console.WriteLine("-------- Outer HTML:");
        Console.WriteLine(tree.DocumentElement.OuterHtml);
        Console.WriteLine("--------\n");

        Console.WriteLine("-------- Text Content of each element:");
        foreach (var element in tree.All)
        {
            Console.WriteLine($"{element.GetType()} = '{element.TextContent}'\n");
        }
        Console.WriteLine("--------\n");

        string selector = "body";
        var elements = tree.QuerySelectorAll(selector);
        Console.WriteLine($"Number of '{selector}' tags = {elements.Length}");

     }


}
