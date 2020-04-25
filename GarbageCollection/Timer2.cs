// Timer2.cs
//
// Demonstration of non-intuitive GC behavior.
//
// Build
//  csc /debug Timer2.cs
//  csc /optimize+ Timer2.cs

using System;
using System.Threading;

public sealed class Program
{
    public static void Main()
    {
        // timer callback executed every 2 seconds
        Timer t = new Timer(TimerCallback, null, 0, 2000);

        Console.ReadLine();

        // forces the timer to GC not to collect timer until this point 
        t.Dispose();
    }

    private static void TimerCallback(Object o)
    {
        Console.WriteLine("Timer Callback");

        GC.Collect();
    }
}

