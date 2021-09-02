using Autofac;
using Maruko.Core.FreeSql.Internal.Context;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace Maruko.TaskScheduling
{
    public class TaskSchedulingModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
            if (AppConfig.Default.Schedule.EnableDatabaseMigrate)
                scope.Resolve<IFreeSqlContext>().GetSet().CodeFirst.SyncStructure(
                    typeof(TaskScheduling),
                    typeof(AllCountryOilPrice)
                );
        }

        public override void ConfigureServices(IServiceCollection service)
        {
            service.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        }

        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterType<OilPriceStrategy>().As<IStrategy>();
        }
    }
}