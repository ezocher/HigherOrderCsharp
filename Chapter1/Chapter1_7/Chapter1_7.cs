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
        IHtmlDocument tree = parser.ParseDocument(HTMLSamples.DocumentEmptyHeader+HTMLSamples.Simple_Page26+HTMLSamples.DocumentClosingTags);
        return tree;
    }

    public static void Demo_First_Parse()
    {
        Console.WriteLine("\n--------------- Chapter 1.7 First parse ---------------");

        var tree = FirstParse();
        Console.WriteLine(tree.DocumentElement.OuterHtml);
        Console.WriteLine("--------");
        foreach (var element in tree.All)
        {
            Console.WriteLine($"{element.GetType()} = '{element.TextContent}'\n");
        }
        Console.WriteLine("--------");
        string selector = "meta";
        var elements = tree.QuerySelectorAll(selector);
        Console.WriteLine($"Number of '{selector}' tags = {elements.Length}");

     }


}
