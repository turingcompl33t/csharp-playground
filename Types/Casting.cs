// Casting.cs
// Basic C# casting rules.

using System;

public sealed class Program
{
    public static void Main()
    {
        Object o1 = new Object();

        // implicit conversion to base type is allowed
        Object o2 = new Base();

        // cast required for explicit conversion to derived type
        Base b = (Base) o2;

        if (b is Object)
        {
            // succeeds because Base is derived from Object
            Console.WriteLine("base is Object");
        }

        if (b is Base)
        {
            // succeeds because Base is the true type of b
            Console.WriteLine("base is Base");
        }

        if (!(o1 is Base))
        {
            // Object is not derived from Base
            Console.WriteLine("o1 is not Base");
        }

        // another casting method using the as operator 
        Base b2 = o1 as Base;
        if (b2 == null)
        {
            Console.WriteLine("o1 is not Base");
        }

        // why does this code compile?
        // Int64 and Int32 are completely distinct types,
        // yet an implicit conversion from Int32 to Int64 is allowed
        // this feels OK because most programming languages
        // support implicit widening conversions, thus the 
        // C# compiler has special rules here for such primitive types
        Int32 i = 5;
        Int64 j = i;
    }
}

// implicitly inherits from System.Object
public class Base
{

}