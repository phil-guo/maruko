using System;
using Autofac;
using Maruko.Core.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.TaskScheduling
{
    public class TaskSchedulingModule : KernelModule
    {
        public override void ConfigureServices(IServiceCollection collection)
        {
            base.ConfigureServices(collection);
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
            base.RegisterModule(builder);
        }
    }
}
