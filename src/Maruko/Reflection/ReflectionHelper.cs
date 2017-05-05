using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyModel;

namespace Maruko.Reflection
{
    public class ReflectionHelper
    {
        /// <summary>
        /// 获取所有的程序集
        /// </summary>
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();

            var libs = DependencyContext.Default.CompileLibraries;

            foreach (CompilationLibrary lib in libs)
            {
                if (lib.Serviceable)
                    continue;

                if (lib.Type == "package")
                    continue;

                var assembly = Assembly.Load(new AssemblyName(lib.Name));

                assemblies.Add(assembly);
            }

            return assemblies;
        }
    }
}
