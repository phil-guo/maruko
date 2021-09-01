using System;
using Autofac;
using Maruko.Core.FreeSql.Internal.Context;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Dynamic.Config
{
    public class DynamicModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
            if (AppConfig.Default.DynamicConfig.EnableDatabaseMigrate)
                scope.Resolve<IFreeSqlContext>().GetSet().CodeFirst.SyncStructure(
                    typeof(Page),
                    typeof(PageConfig)
                );
        }
    }
}
