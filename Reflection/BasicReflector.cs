// BasicReflector.cs
// Trivial assembly reflector.

using System;
using System.Reflection;

public sealed class Program
{
    public static void Main()
    {
        // enumerate all assemblies in the current appdomain
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly a in assemblies)
        {
            Print(0, "Assembly: {0}", a);

            // enumerate all exported types in the assembly
            foreach (Type t in a.ExportedTypes)
            {
                Print(1, "Type: {0}", t);

                // enumerate all declared members for the type
                foreach (MemberInfo mi in t.GetTypeInfo().DeclaredMembers)
                {
                    String typeName = String.Empty;
                    if (mi is Type) typeName = "(Nested) Type";
                    if (mi is FieldInfo) typeName = "FieldInfo";
                    if (mi is MethodInfo) typeName = "MethodInfo";
                    if (mi is ConstructorInfo) typeName = "ConstructorInfo";
                    if (mi is PropertyInfo) typeName = "PropertyInfo";
                    if (mi is FieldInfo) typeName = "FieldInfo";
                    Print(2, "{0}: {1}", typeName, mi);
                }
            }
        }
    }

    public static void Print(
        Int32 indent,
        String format,
        params Object[] args
    )
    {
        Console.WriteLine(new String(' ', 3*indent) + format, args);
    }
}