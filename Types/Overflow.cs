// Overflow.cs
// Demo of integral overflow behavior in C#.

using System;

public sealed class Program
{
    public static void Main()
    {
        Byte l = 100;

        // unchecked addition
        Byte u = (Byte) (l + 200);

        Console.WriteLine("Result of unchecked addition = " + u.ToString());

        // checked addition
        try
        {
            Byte c = checked((Byte) (l + 200));
        }
        catch (OverflowException)
        {
            Console.WriteLine("Caught System.OverflowException");
        }
    }
}