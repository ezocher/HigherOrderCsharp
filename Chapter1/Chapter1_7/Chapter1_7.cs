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
        IHtmlDocument tree = parser.ParseDocument(HTMLSamples.Simple_Page26);
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
        var elements = tree.QuerySelectorAll("font");
        Console.WriteLine($"Number of font tags = {elements.Length}");

    }


}

public class HTMLSamples
{
    public const string EmptyHeader = @"
<!DOCTYPE html> 
<html> 
<head> 
    <title></title> 
</head> 
<body>
";

    public const string EmptyFooter = @"
</body> 
</html>
";

    public const string Simple_Page26 = @"
<h1>What Junior Said Next</h1>

<p>But I don't <font size=3 color=""red"">want</font>
to go to bed now</p>";

}