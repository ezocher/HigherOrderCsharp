/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;

class Chapter1_2_1
{
    static int n = 1;

    // factorial-broken - Higher Order Perl p. 5
    static int Factorial_Broken(int i)
    {
        n = i;
        if (n == 0) return 1;
        return Factorial_Broken(n - 1) * n;
    }

    public static void Demo_Factorial_Broken()
    {
        Console.WriteLine("\n--------------- Chapter 1.2.1 ---------------");
        for (int i = 0; i <= 10; i++)
            Console.WriteLine("{0}! = {1:N0}", i, Factorial_Broken(i));
    }
}