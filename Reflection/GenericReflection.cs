// GenericReflection.cs
// Using reflection to analyze generic collections.

using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public sealed class Program
{
    public static void Main()
    {
        Assembly mscorlib = Assembly.Load("mscorlib");
        
        IEnumerable<String> types = mscorlib.GetTypes()
            .Where(t => t.Namespace == null ? false : t.Namespace.Contains("System.Collections"))
            .Select(t => t.Name);

        foreach (String t in types)
        {
            Console.WriteLine(t);
        }
    }
}