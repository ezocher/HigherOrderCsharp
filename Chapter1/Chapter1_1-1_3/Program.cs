/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */
 
using System;
class Program
{
    static void Main(string[] args)
    {
        Chapter1_1.Demo_Binary();
        Chapter1_2.Demo_Factorial();
        Chapter1_2_1.Demo_Factorial_Broken();
        Chapter1_3.Demo_Hanoi_Original();
        Chapter1_3.Demo_Hanoi();
        Chapter1_3.Demo_Check_Move();
    }
}


/* Some resources for background information about topics in Chapter 1
 *
 * Memoization: 
 *      https://spin.atomicobject.com/2011/10/27/generic-memoization-in-c/
 *      https://www.aleksandar.io/post/memoization/
 *      
 * Aspect-oriented programming:
 *      https://www.dotnetcurry.com/patterns-practices/1305/aspect-oriented-programming-aop-csharp-using-solid
 *      https://www.postsharp.net/blog/post/Anders-Hejlsberg-Dead-Body
 *      https://msdn.microsoft.com/en-us/magazine/dn574804.aspx?f=255&MSPPError=-2147217396
 *      
 * Dependency injection:
 *
 */
