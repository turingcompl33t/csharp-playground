// Primitives.cs
// Demo of some C# primitive types.

public sealed class Program
{
    public static void Main()
    {
        // all of these statements produce exactly the same IL
        // int is a primitive type - it is supported directly by the 
        // compiler and is mapped one-to-one with the FCL type 
        // Int32 defined in the System namespace
        int          a = 0;
        System.Int32 b = 0;
        int          c = new int();
        System.Int32 d = new System.Int32();

        // design decision:
        // should one use the primitive types supported
        // by the C# compiler? or simply always use the 
        // FCL type?
    }
}