using System;
using System.IO;
using System.Reflection;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;
using Maruko.Core.Web.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace Maruko.Core.Web
{
    public class WebModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
        }
    }
}
