using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Maruko.Core.Extensions;

namespace Maruko.Core.Reflection
{
    public static class TypeFindExtensions
    {
        public static Dictionary<string, List<Assembly>> DictionaryAssembly;

        public static void AddAssmbly(Assembly assembly)
        {
            if (DictionaryAssembly == null)
                DictionaryAssembly = new Dictionary<string, List<Assembly>>();
            if (!DictionaryAssembly.ContainsKey("Assembly"))
                DictionaryAssembly.Add("Assembly", new List<Assembly>());
            DictionaryAssembly["Assembly"].Add(assembly);
        }

        private static readonly object SyncObj = new object();
        private static Type[] _types;

        public static Type[] Find(Func<Type, bool> predicate)
        {
            return GetAllTypes().Where(predicate).ToArray();
        }

        private static Type[] GetAllTypes()
        {
            if (_types == null)
            {
                lock (SyncObj)
                {
                    if (_types == null)
                    {
                        _types = CreateTypeList().ToArray();
                    }
                }
            }

            return _types;
        }

        private static List<Type> CreateTypeList()
        {
            var allTypes = new List<Type>();

            var assemblies = GetAllAssemblies().Distinct();

            foreach (var assembly in assemblies)
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return allTypes;
        }

        private static List<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var assembly in ReflectionHelper.GetAssemblies())
            {
                assemblies.Add(assembly);
            }

            return assemblies.Distinct().ToList();
        }
    }
}
