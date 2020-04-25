// Timer1.cs
//
// Demonstration of non-intuitive GC behavior.
//
// Build
//  csc /debug Timer1.cs
//  csc /optimize+ Timer1.cs

using System;
using System.Threading;

public sealed class Program
{
    public static void Main()
    {
        // timer callback executed every 2 seconds
        Timer t = new Timer(TimerCallback, null, 0, 2000);

        Console.ReadLine();
    }

    private static void TimerCallback(Object o)
    {
        Console.WriteLine("Timer Callback");

        GC.Collect();
    }
}

