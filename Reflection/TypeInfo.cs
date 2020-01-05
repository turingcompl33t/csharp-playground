// TypeInfo.cs
// Distinction between Type and TypeInfo types.

using System;
using System.Reflection;

public sealed class Program
{
    public static void Main()
    {
        // get a type reference
        Type t = typeof(Object);

        // get the (more heavyweight) type definition
        TypeInfo ti = t.GetTypeInfo();

        Console.WriteLine(ti.AssemblyQualifiedName);

        foreach (MethodInfo m in ti.GetMethods())
        {
            Console.WriteLine(" " + m.Name);
        }
    }
}