// Volatile1.cs
//
// Behavior only observed with optimizations
// and the x86 JIT compiler.
// ^Actually no, x64 JIT now performs this optimization as well.
//
// Build
//  csc /platform:x86 /optimize+ Volatile1.cs

using System;
using System.Threading;

public sealed class Program
{
    static bool stopWorker = false;
    public static void Main()
    {
        Console.WriteLine("Launching worker to run for 5 seconds");
        Thread t = new Thread(Worker);
        t.Start();
        Thread.Sleep(5000);
        stopWorker = true;
        Console.WriteLine("Worker stopped; waiting for exit");
        t.Join();
    }

    private static void Worker(Object arg)
    {
        Console.WriteLine("Worker started");

        Int32 x = 0;
        while (!stopWorker)
        {
            ++x;
        }
        Console.WriteLine("Worker stopped at value {0}", x);
    }
}