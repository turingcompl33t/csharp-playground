// WhenAll.cs
//
// Messing with TPL.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public sealed class Program
{
    public static void Main()
    {
        List<Task> tasks = new List<Task>();

        tasks.Add(Task.Run(() => Worker(1000)));
        tasks.Add(Task.Run(() => Worker(3000)));

        Task t3 = Task.WhenAll(tasks);
        t3.Wait();

        Console.WriteLine("All tasks completed");
    }

    private static void Worker(Int32 ms)
    {
        Int32 id = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine("Entered thread {0}", id);
        Thread.Sleep(ms);
        Console.WriteLine("Exiting thread {0}", id);
    }
}