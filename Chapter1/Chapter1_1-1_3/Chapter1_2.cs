/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;
using System.Numerics;

class Chapter1_2
{
    // If we try using int or long they will silently overflow pretty quickly (see output from Demo() below to see how quickly)
    //
    // System.Numerics.BigInteger gives us arbitrary size integers, which will work better than a Perl scalar (Perl uses 
    //  double-precision floating point to represent all numbers)

    // factorial - Higher Order Perl p. 4
    static BigInteger Factorial(BigInteger n)
    {
        if (n == 0) return 1;
        return Factorial(n - 1) * n;
    }

    private const int maxFactorialToDemo = 100;
    public static void Demo_Factorial()
    {
        Console.WriteLine("\n--------------- Chapter 1.2 ---------------");
        for (int i = 0; i <= maxFactorialToDemo; i++)
            Console.WriteLine("{0}! = {1:N0}", i, Factorial(i));

        // int silently overflows at 13!
        // long silently overflows at 21!

        int j = 0;
        while (Factorial(j) <= Int32.MaxValue) j++;
        Console.WriteLine("\nInt32 max value = {0:N0} - Factorial overflows at {1}! = {2:N0}", Int32.MaxValue, j, Factorial(j));

        while (Factorial(j) <= Int64.MaxValue) j++;
        Console.WriteLine("\nInt64 max value = {0:N0} - Factorial overflows at {1}! = {2:N0}", Int64.MaxValue, j, Factorial(j));
    }
}
