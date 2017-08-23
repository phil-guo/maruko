using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Maruko.Modules;
using Microsoft.Extensions.DependencyModel;

namespace Maruko.Reflection
{
    public class ReflectionHelper
    {
        /// <summary>
        ///     获取所有的程序集
        /// </summary>
        public static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();

            var libs = DependencyContext.Default.CompileLibraries;

            foreach (var lib in libs)
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

        public static IEnumerable<Assembly> GetAssembliyList()
        {
            var assemblies = new List<Assembly>();

            var assemblyNameLoads = GetDependsOnCompantName().Distinct().ToList();

            assemblyNameLoads.ForEach(item =>
            {
                var assembly = Assembly.Load(new AssemblyName(item));

                assemblies.Add(assembly);
            });
            return assemblies;
        }

        private static List<string> GetDependsOnCompantName()
        {
            var types = TypeFindExtensions.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsDefined(typeof(LoadOnAttribute));
                }
            );

            var typeStr = new List<string>();

            foreach (var type in types)
            {
                var typeAttribute = type.GetTypeInfo().GetCustomAttribute<LoadOnAttribute>();

                if (!typeAttribute.IsAuto)
                    continue;

                typeStr.AddRange(typeAttribute.DependedModuleTypes.Select(moduleName => moduleName.ToLower()));
            }

            return typeStr;
        }
    }
}