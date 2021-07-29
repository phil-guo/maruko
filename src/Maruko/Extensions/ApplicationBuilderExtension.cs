﻿using Autofac;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;

namespace Maruko.Core.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseMaruko(this IApplicationBuilder app)
        {
            var kernelModule = AutofacExtensions.Current.Resolve<IKernelModuleProvider>();
            kernelModule.Initialize();
            return app;
        }
    }
}
