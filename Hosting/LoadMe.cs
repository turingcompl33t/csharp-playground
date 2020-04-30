// LoadMe.cs
//
// Demo application that defines a method for invocation in custom CLR host.
//
// Build
//  csc /target:library /out:LoadMe.dll LoadMe.cs

using System;

class LoadMe
{
    public static Int32 InvokeMe(String arg)
    {
        Console.WriteLine("Received Argument: {0}", arg);
        return 1337;
    }
}
