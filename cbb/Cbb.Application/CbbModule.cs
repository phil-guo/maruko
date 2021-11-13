using System;
using Autofac;
using Maruko.Core.FreeSql.Internal.Context;
using Maruko.Core.Modules;
using Microsoft.AspNetCore.Builder;

namespace Cbb.Application
{
    public class CbbModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
            if (AppConfig.Default.Cbb.EnableDatabaseMigrate)
                scope.Resolve<IFreeSqlContext>().GetSet().CodeFirst.SyncStructure(
                    typeof(Banner)
                );
        }
    }
}