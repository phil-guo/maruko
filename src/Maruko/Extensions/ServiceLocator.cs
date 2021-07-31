using System;
using System.Linq;
using Autofac;
using Maruko.Core.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Core.Extensions
{
    public static class ServiceLocator
    {
        public static ILifetimeScope Current { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }
    }
}