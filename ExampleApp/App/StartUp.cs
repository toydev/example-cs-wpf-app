﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.IO;

namespace ExampleApp.App
{
    public class StartUp
    {
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;

            App.Main(); // Run WPF startup code.
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs e)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            // Get the Name of the AssemblyFile
            var assembly = new AssemblyName(e.Name);
            var dllName = assembly.Name + ".dll";

            // Load satelite assembly
            if (dllName.EndsWith("resources.dll") && !CultureInfo.InvariantCulture.Equals(assembly.CultureInfo))
            {
                var sateliteAssembly = LoadAssemblyFromResource(thisAssembly, assembly.CultureInfo.Name + @"\" + dllName);
                if (sateliteAssembly != null)
                {
                    return sateliteAssembly;
                }
            }

            // Load normal assembly
            return LoadAssemblyFromResource(thisAssembly, dllName);
        }

        private static Assembly LoadAssemblyFromResource(Assembly thisAssembly, string dllName)
        {
            // Load from Embedded Resources - This function is not called if the Assembly is already
            // in the same folder as the app.
            var resourceName = thisAssembly.GetManifestResourceNames().Where(
                s => s.EndsWith(dllName, true, null)).FirstOrDefault();

            if (resourceName != null)
            {
                using (var stream = thisAssembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        return null;
                    }

                    try
                    {
                        var block = new byte[stream.Length];
                        stream.Read(block, 0, block.Length);
                        return Assembly.Load(block);
                    }
                    catch (IOException)
                    {
                        return null;
                    }
                    catch (BadImageFormatException)
                    {
                        return null;
                    }
                }
            }

            // in the case the resource doesn't exist, return null.
            return null;
        }
    }
}
