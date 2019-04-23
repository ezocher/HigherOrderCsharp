/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;

class Chapter1_1
{
    // In Perl, scalars (like $n, $k, $b, and $E) can represent numbers or strings interchangably
    // In C# we use specific types: int to do the calculations, and string to accumulate the binary expansion of 0s and 1s

    // binary - Higher Order Perl p. 2
    static string Binary(int n)
    {
        if ((n == 0) || (n == 1)) return n.ToString();
        int k = n / 2;
        int b = n % 2;
        string E = Binary(k);
        return E + b.ToString();
    }

    public static void Demo_Binary()
    {
        Console.WriteLine("\n--------------- Chapter 1.1 ---------------");
        int[] demoValues = { 0, 1, 7, 11, 16, 37, 99, 170, 37*16, 32767 };
        foreach (int v in demoValues)
            Console.WriteLine("{0} decimal = {0:X} hexadecimal = {1} binary", v, Binary(v));
    }
}
