// Cancellation.cs
//
// Cooperative thread cancellation.

using System;
using System.Threading;

public sealed class Program
{
    public static void Main()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        ThreadPool.QueueUserWorkItem(o => Worker(cts.Token, 10000));

        Console.WriteLine("ENTER to cancel worker thread");
        Console.ReadLine();

        cts.Cancel();

        Console.ReadLine();
    }

    private static void Worker(CancellationToken token, Int32 count)
    {
        for (Int32 i = 0; i < count; ++i)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Worker thread cancelled");
                break;
            }

            Console.WriteLine("Count: {0}", i);
            Thread.Sleep(200);
        }
        Console.WriteLine("Exiting worker");
    }
}