// Embedded.cs
//
// Build
//  csc /target:library /out:Embedded.dll Embedded.cs

using System;

public sealed class Embedded
{
    public static Int32 Add(Int32 a, Int32 b)
    {
        return a + b;
    }
}