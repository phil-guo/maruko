using System;
using System.Linq;
using Autofac;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Core.Extensions
{
    public static class ServiceLocator
    {
        public static ILifetimeScope Current { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }
        public static IApplicationBuilder App { get; set; }
        public static IConfiguration Configuration { get; set; }
    }
}