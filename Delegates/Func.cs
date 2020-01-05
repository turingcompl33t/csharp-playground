// Func.cs
// Utilizing the Func generic delegate.

using System;
using System.Collections.Generic;

public sealed class Program
{
    public static void Main()
    {
        Int32[] ints = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        TransformAndPrint(ints, x => x*2);
        TransformAndPrint(ints, x => x+11);
    }

    public static void TransformAndPrint(
        IEnumerable<Int32> ints,
        Func<Int32, Int32> func
    )
    {
        List<Int32> results = new List<Int32>();
        foreach (Int32 i in ints)
        {
            results.Add(func(i));
        }
        PrintListContents(results);
    }

    public static void PrintListContents(List<Int32> l)
    {
        foreach (Int32 i in l)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine("");
    }
}