// Host.cs
//
// Build
//  csc /target:module /out:Host.netmodule /reference:Embedded.dll Host.cs
//  al /target:exe /out:Host.exe /main:Host.Main /embed:Embedded.dll Host.netmodule

using System;
using System.IO;
using System.Reflection;

public sealed class Host
{
    public static void Main()
    {
        // when assembly resolution fails, the AppDomain raises AssemblyResolve event
        // we can register an event handler to catch the failed resolution and 
        // manually load the desired assembly from the resource section of our binary
        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {

            String resourceName = new AssemblyName(args.Name).Name + ".dll";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                Byte[] data = new Byte[stream.Length];

                stream.Read(data, 0, data.Length);

                return Assembly.Load(data);
            }
        };

        // uses type / static method defined in embedded library
        Int32 r = Embedded.Add(1000, 337);

        // did it work?
        Console.WriteLine("1000 + 337 = {0}", r);
    }
}