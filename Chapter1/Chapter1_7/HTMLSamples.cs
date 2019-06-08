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

public class HTMLSamples
{
    const string DocumentEmptyHeader = @"
<!DOCTYPE html> 
<html> 
<head> 
    <title>HTML Sample Document</title> 
</head> 
<body>
";

    const string DocumentClosingTags = @"
</body> 
</html>
";

    public const string Simple_Page26 = @"
<h1>What Junior Said Next</h1>

<p>But I don't <font size=3 color=""red"">want</font>
to go to bed now</p>";

    public static string EncloseInHeaderFooter(string htmlBody)
    {
        return DocumentEmptyHeader + htmlBody + DocumentClosingTags;
    }
}