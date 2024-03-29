﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Maruko.Core.Reflection
{
    public static class ReflectionHelper
    {
        public static List<Assembly> Assemblies { get; set; }

        public static void GetAssemblyArray(Assembly assembly)
        {
            if (Assemblies == null)
                Assemblies = new List<Assembly>();

            Assemblies.Add(assembly);
        }

        /// <summary>
        ///     获取所有的程序集
        /// </summary>
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();

            var libs = Assembly.GetEntryAssembly().GetReferencedAssemblies();

            foreach (var lib in libs)
            {
                //if (lib.Serviceable)
                //    continue;

                //if (lib.Type == "package")
                //    continue;

                var assembly = Assembly.Load(new AssemblyName(lib.Name));

                assemblies.Add(assembly);
            }

            return assemblies;
        }
    }
}