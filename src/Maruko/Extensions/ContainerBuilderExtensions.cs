using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Autofac;
using Maruko.Core.Config;
using Maruko.Core.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;

namespace Maruko.Core.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static readonly List<Assembly> ReferenceAssembly = new List<Assembly>();
        private static readonly List<KernelModule> Modules = new List<KernelModule>();

        /// <summary>
        /// 批量注册模块
        /// </summary>
        public static void RegisterModules(this ContainerBuilder builder, IConfiguration appConfig)
        {
            var referenceAssemblies = GetAssemblies(appConfig);
            foreach (var moduleAssembly in referenceAssemblies)
            {
                GetKernelModules(moduleAssembly).ForEach(module =>
                {
                    builder.RegisterModule(module);
                    Modules.Add(module);
                });
            }

            builder.RegisterType<KernelModuleProvider>().As<IKernelModuleProvider>()
                .WithParameter(new TypedParameter(typeof(List<KernelModule>), Modules));
        }



        private static List<Assembly> GetAssemblies(IConfiguration appConfig)
        {
            var  referenceAssemblies = new List<Assembly>();

            var assemblyNames = DependencyContext
                .Default.GetDefaultAssemblyNames().Select(p => p.Name).ToArray();
            assemblyNames = GetFilterAssemblies(assemblyNames, appConfig);
            foreach (var name in assemblyNames)
                referenceAssemblies.Add(Assembly.Load(name));
            ReferenceAssembly.AddRange(referenceAssemblies.Except(ReferenceAssembly));

            return referenceAssemblies;
        }

        private static string[] GetFilterAssemblies(string[] assemblyNames, IConfiguration appConfig)
        {
            var pattern = "^System\\w*|^Quartz\\w*|^Autofac\\w*|^Microsoft.\\w*|^System.\\w*|^AutoMapper\\w*|^Newtonsoft.\\w*|^Autofac.\\w*|^Swashbuckle.\\w*|^FreeSql.\\w*|^FreeSql\\w*|^Nito.\\w*|^xunit.\\w*|^k4os.\\w*|^BouncyCastle.\\w*|^Google.\\w*|^Renci.\\w*|^SshNet.\\w*|^MySql.\\w*|^Ubiety.\\w*|^Zstandard.\\w*|^testhost\\w*|^WindowsBase\\w*|^NuGet.\\w*|^Humanizer\\w*";
            var app = new AppConfig(appConfig);
            pattern = string.IsNullOrEmpty(app.Core.ExcludeModules)
                ? pattern
                : $"{app.Core.ExcludeModules}|{pattern}";

            var notRelatedRegex = new Regex(pattern,
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return
                assemblyNames.Where(
                    name => !notRelatedRegex.IsMatch(name)).ToArray();
        }

        private static List<KernelModule> GetKernelModules(Assembly assembly)
        {
            var modules = new List<KernelModule>();
            Type[] arrayModule =
                assembly
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(KernelModule)))
                    .ToArray();

            foreach (var moduleType in arrayModule)
            {
                var abstractModule = (KernelModule)Activator.CreateInstance(moduleType);
                modules.Add(abstractModule);
            }

            return modules;
        }
    }
}
