// ObserverWithEvent.cs
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
    // event keyword here prohibits one 
    // from invoking the delegate directly
    // and from assigning to the delegate reference directly
    public event Action SignalLit;

    public void SubscribeToSignal(Action a)
    {
        SignalLit += a;
    }

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
        signal.SubscribeToSignal(this.Stop);
    }

    public void Stop()
    {
        Console.WriteLine("Car {0} Stopped!", Id);
    }

    public Int16 Id { get; set; }
}