using System;
using Autofac;
using Maruko.Core.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.TaskScheduling
{
    public class TaskSchedulingModule : KernelModule
    {
        public override void ConfigureServices(IServiceCollection service)
        {
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
        }
    }
}
