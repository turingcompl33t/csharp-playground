// Action.cs
// Utilizing the Action generic delegate.

using System;
using System.Collections.Generic;

public sealed class Program
{
    public static void Main()
    {
        Int32[] ints = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        ForEachWithSideEffect(ints, x => Console.WriteLine("side effect: " + x));
    }

    public static void ForEachWithSideEffect(
        IEnumerable<Int32> ints,
        Action<Int32>      action
    )
    {
        foreach (Int32 i in ints)
        {
            action(i);
        }
    }
}