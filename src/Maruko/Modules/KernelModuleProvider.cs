﻿using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Core.Modules
{
    public class KernelModuleProvider : IKernelModuleProvider
    {
        private readonly List<KernelModule> _modules;

        public KernelModuleProvider(List<KernelModule> modules)
        {
            _modules = modules;
        }

        public void Initialize(IApplicationBuilder app)
        {
            this.TryCatch(() => { _modules.ForEach(item => { item.Initialize(ServiceLocator.Current, app); }); },
                "模块初始化错误");
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            this.TryCatch(() => { _modules.ForEach(item => { item.ConfigureServices(collection); }); },
                "模块初始化错误");
        }
    }
}