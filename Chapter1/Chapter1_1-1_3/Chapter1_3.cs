/* 
 * https://github.com/ezocher/HigherOrderCsharp
 * 
 * C# implementation of the code from Higher Order Perl by Mark Jason Dominus
 * https://hop.perl.plover.com/
 * 
 */

using System;

class Chapter1_3
{
    #region hanoi-original

    // hanoi (original) - Higher Order Perl p. 8
    static void Hanoi_Original(int n, char start, char end, char extra)
    {
        if (n == 1)
            Console.WriteLine("Move disk #{0} from {1} to {2}", n, start, end);
        else
        {
            Hanoi_Original(n - 1, start, extra, end);
            Console.WriteLine("Move disk #{0} from {1} to {2}", n, start, end);
            Hanoi_Original(n - 1, extra, end, start);
        }
    }

    public static void Demo_Hanoi_Original()
    {
        Console.WriteLine("\n--------------- Chapter 1.3 Original ---------------");
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine("Number of discs = {0}", i);
            Hanoi_Original(i, 'A', 'C', 'B');
            Console.WriteLine();
        }
    }
    #endregion hanoi-original

    #region hanoi

    // hanoi - Higher Order Perl p. 9
    static void Hanoi(int n, char start, char end, char extra, Mover moveDisc)
    {
        if (n == 1)
            moveDisc(1, start, end);
        else
        {
            Hanoi(n - 1, start, extra, end, moveDisc);
            moveDisc(n, start, end);
            Hanoi(n - 1, extra, end, start, moveDisc);
        }
    }

    delegate void Mover(int x, char from, char to);

    static void Print_Instruction(int x, char from, char to)
    {
        Console.WriteLine("Move disk #{0} from {1} to {2}", x, from, to);
    }


    public static void Demo_Hanoi()
    {
        Console.WriteLine("\n--------------- Chapter 1.3 ---------------");
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine("Number of discs = {0}", i);
            Hanoi(i, 'A', 'C', 'B', Print_Instruction);
            Console.WriteLine();
        }
    }
    // We could also call Hanoi with an expression lambda instead of calling Print_Instruction():
    //      Hanoi(i, 'A', 'C', 'B', (x, from, to) => Console.WriteLine("Move disk #{0} from {1} to {2}", x, from, to));            

    #endregion hanoi

    #region check-move

    static char[] position = { ' ', 'A', 'A', 'A' };    // Disks are all initially on peg A

    // check-move - Higher Order Perl pp. 10 - 11
    static void Check_Move(int disk, char start, char end)
    {
        if ((disk < 1) || (disk > (position.Length - 1)))
            throw new Exception(String.Format("Bad disk number {0}. Should be 1..{1}.", disk, position.Length - 1));
        if (position[disk] != start)
            throw new Exception(String.Format("Tried to move disk {0} from {1}, but it is on peg {2}.", disk, start, position[disk]));

        for (int i = 1; i <= disk - 1; i++)
        {
            if (position[i] == start)
                throw new Exception(String.Format("Can't move disk {0} from {1} because {2} is on top of it.", disk, start, i));
            else if (position[i] == end)
                throw new Exception(String.Format("Can't move disk {0} to {1} because {2} is already there.", disk, end, i));
        }

        Console.WriteLine("Moving disk #{0} from {1} to {2}", disk, start, end);
        position[disk] = end;
    }

    public static void Demo_Check_Move()
    {
        Console.WriteLine("\n--------------- Chapter 1.3 with Check_Move ---------------");
        int i = 3;  // Implementation in the book is for three disks only because of fixed size position array
        Console.WriteLine("Number of discs = {0}", i);
        Hanoi(i, 'A', 'C', 'B', Check_Move);
        Console.WriteLine();
    }
    #endregion check-move

}
