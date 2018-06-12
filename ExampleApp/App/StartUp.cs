using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;

namespace ExampleApp.App
{
    public class StartUp
    {
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            // AppDomain.CurrentDomain.ResourceResolve += OnResolveResource;

            MainApplication.Main();
        }

        // DLL 埋め込み関連

        private static readonly ISet<string> LoadAssemblies = new HashSet<string>();

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs e)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            // Get the Name of the AssemblyFile
            var assembly = new AssemblyName(e.Name);
            var dllName = assembly.Name + ".dll";

            using (var writer = new StreamWriter("OnResolveAssembly.log", true, Encoding.GetEncoding("UTF-8")))
            {
                Assembly result = null;
                if (assembly.CultureInfo != null)
                {
                    result = LoadAssemblyFromResource(writer, thisAssembly, assembly.CultureInfo.Name + @"\" + dllName);
                    if (result != null)
                    {
                        return result;
                    }

                    result = LoadAssemblyFromResource(writer, thisAssembly, assembly.CultureInfo.TwoLetterISOLanguageName + @"\" + dllName);
                    if (result != null)
                    {
                        return result;
                    }
                }

                result = LoadAssemblyFromResource(writer, thisAssembly, dllName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private static Assembly LoadAssemblyFromResource(StreamWriter writer, Assembly thisAssembly, string dllName)
        {
            var resourceName = thisAssembly.GetManifestResourceNames().Where(
                s => s.EndsWith(dllName, true, null)).FirstOrDefault();

            writer.Write(string.Format("DLL {0} => Resource {0}: ", dllName, resourceName));

            if (resourceName != null)
            {
                using (var stream = thisAssembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        writer.WriteLine("not read");
                        return null;
                    }

                    var block = new byte[stream.Length];

                    try
                    {
                        stream.Read(block, 0, block.Length);
                        writer.WriteLine("read");
                        return Assembly.Load(block);
                    }
                    catch (IOException)
                    {
                        writer.WriteLine("not read(IOException)");
                        return null;
                    }
                    catch (BadImageFormatException)
                    {
                        writer.WriteLine("not read(BadImageFormatException)");
                        return null;
                    }
                }
            }

            writer.WriteLine("not read(no resource)");
            return null;
        }

        private static Assembly OnResolveResource(object sender, ResolveEventArgs e)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            // Get the Name of the AssemblyFile
            var assemblyName = new AssemblyName(e.Name);
            var dllName = assemblyName.Name + ".dll";

            using (var writer = new StreamWriter("OnResolveResource.log", true, Encoding.GetEncoding("UTF-8")))
            {
                writer.WriteLine(string.Format("Start {0} {1}", assemblyName, dllName));
            }

            return null;
            /*
            try
            {
                if (args.Name.StartsWith("your.resource.namespace"))
                {
                    return LoadResourcesAssyFromResource(System.Threading.Thread.CurrentThread.CurrentUICulture, "name of your the resource that contains dll");
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            */
        }

        /*
        private Assembly LoadResourceFromResource(CultureInfo culture, ResourceName resName)
        {
            //var x = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName))
            {
                if (stream == null)
                {
                    //throw new Exception("Could not find resource: " + resourceName);
                    return null;
                }

                Byte[] assemblyData = new Byte[stream.Length];

                stream.Read(assemblyData, 0, assemblyData.Length);

                var ass = Assembly.Load(assemblyData);

                return ass;
            }
        }
        */
    }
}
