// ObserverWithDelegate.cs
//
// Basic observer pattern via C# delegates.

using System;

public sealed class Program
{
    public static void Main()
    {
        TrainSignal signal = new TrainSignal();
        for (Int16 i = 0; i < 5; i++)
        {
            new Car(i, signal);
        }

        signal.TrainIsComing();
    }
}

public sealed class TrainSignal
{   
    public Action SignalLit;

    public void TrainIsComing()
    {
        SignalLit();
    }
}

public sealed class Car
{
    public Car(Int16 id, TrainSignal signal)
    {
        Id = id;
        signal.SignalLit += this.Stop; 
    }

    public void Stop()
    {
        Console.WriteLine("Car {0} Stopped!", Id);
    }

    public Int16 Id { get; set; }
}