using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Maruko.Dependency.Installers;
using Maruko.Modules;
using Maruko.Reflection;

namespace Maruko.Extensions
{
    public static class AutofacExtensions
    {
        public static void RegisterModulesExtension(this ContainerBuilder containerBuilder, Func<MarukoModule[]> func)
        {
            func().OrderBy(item=>item.Order).ToList().ForEach(module => { containerBuilder.RegisterModule(module); });
        }
    }
}