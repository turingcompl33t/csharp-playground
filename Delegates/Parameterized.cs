// Parameterized.cs
// Using delegates to parameterize code.

using System;
using System.Text;
using System.Collections.Generic;

internal delegate Boolean IntegerFilter(Int32 value);

public sealed class Program
{
    public static void Main()
    {
        Int32[] ints = {1, 2, 3, 4, 5, 6, 7, 8, 9};

        ApplyFilterAndPrint(ints, x => x > 4);
        ApplyFilterAndPrint(ints, x => x % 2 == 0);
    }

    internal static void ApplyFilterAndPrint(
        IEnumerable<Int32> ints, 
        IntegerFilter filter
        )
    {
        StringBuilder s = new StringBuilder();
        foreach(Int32 i in ints)
        {
            if (filter(i))
            {
                s.Append(i.ToString() + " ");
            }
        }

        Console.WriteLine(s.ToString());
    }
}