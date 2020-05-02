// SpinLock1.cs
//
// A simple spin-lock via interlocked operations.

using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

static class Constants
{
    public static Int32 ThreadCount = 5;
    public static Int32 OpsPerThread = 10000;
}

// avoid naming collision with Threading.SpinLock
internal struct SimpleSpinLock
{
    private Int32 m_Locked; // 0 = false (default), 1 = true

    public void Enter()
    {
        while (true)
        {
            // return when this thread changes value from 
            // not-in-use to in-use
            if (Interlocked.Exchange(ref m_Locked, 1) == 0) return;
            
            // black magic here
        }
    }

    public void Leave()
    {
        Volatile.Write(ref m_Locked, 0);
    }
}

internal sealed class SomeResource
{
    private SimpleSpinLock m_Lock = new SimpleSpinLock();

    // the lock is not actually necessary here as there is 
    // and overload of Interlocked.Increment() for Int64
    private Int64 m_Data = 0;

    public void Access()
    {
        m_Lock.Enter();
        
        // complex logic to manipulate resource internals...
        ++m_Data;

        m_Lock.Leave();
    }
    
    public Int64 ReadUnsafe()
    {
        return m_Data;
    }
}

public sealed class Program
{
    public static void Main()
    {
        SomeResource res = new SomeResource();

        List<Thread> threads = new List<Thread>();
        for (var i = 0; i < Constants.ThreadCount; ++i)
        {
            Thread t = new Thread(() => Worker(res));
            t.Start();

            threads.Add(t);
        }

        foreach(Thread t in threads)
        {
            t.Join();
        }

        Debug.Assert(res.ReadUnsafe() == Constants.ThreadCount*Constants.OpsPerThread);
        Console.WriteLine("Success!");
    }

    private static void Worker(Object arg)
    {
        SomeResource res = (SomeResource)arg;

        for (var i = 0; i < Constants.OpsPerThread; ++i)
        {
            res.Access();
        }
    }
}