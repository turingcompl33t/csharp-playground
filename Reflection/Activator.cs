// Activator.cs
// Using System.Reflection.Activator to instantiate type instances.

using System;
using System.Reflection;

internal sealed class Dictionary<Tkey, Tvalue> {}

public sealed class Program
{
    public static void Main()
    {
        Type o = typeof(Object);
        Object oInstance = Activator.CreateInstance(o);
        Console.WriteLine(oInstance.GetType());

        Type openType = typeof(Dictionary<,>);
        Type closedType = openType.MakeGenericType(typeof(String), typeof(Int32));
        Object gInstance = Activator.CreateInstance(closedType);
        Console.WriteLine(gInstance.GetType());
    }
}