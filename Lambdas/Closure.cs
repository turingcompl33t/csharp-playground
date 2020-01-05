// Closure.cs
// Demonstration of basic C# closure behavior.

using System;

public sealed class Program
{
    public static void Main()
    {
        // notice that each time we call GetAction(), 
        // we get a new closure that captures a distinct 
        // version of the local variable i

        Action a = GetAction("A");
        Action b = GetAction("B");
        
        a();
        b();
        a();
        b();
    }

    public static Action GetAction(String id)
    {
        Int32 i = 0;
        return new Action(() => Console.WriteLine(String.Format("{0} New Value Is: {1}", id, ++i)));
    }
}