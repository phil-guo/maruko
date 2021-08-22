using System;
using Autofac;
using Maruko.Core.FreeSql.Internal.Context;
using Maruko.Core.Modules;
using Maruko.Zero.Config;
using Microsoft.AspNetCore.Builder;

namespace Maruko.Zero
{
    public class ZeroModule : KernelModule
    {
        public override void Initialize(ILifetimeScope scope, IApplicationBuilder app)
        {
            Migrate(scope.Resolve<IFreeSqlContext>(), new AppConfig());
            SeedData(scope.Resolve<IFreeSqlContext>(), new AppConfig());
        }

        public void Migrate(IFreeSqlContext context, AppConfig appConfig)
        {
            if (appConfig.Zero.EnableDatabaseMigrate)
                context.GetSet().CodeFirst.SyncStructure(
                    typeof(SysUser),
                    typeof(SysMenu),
                    typeof(SysOperate),
                    typeof(SysRole),
                    typeof(SysRoleMenu),
                    typeof(SysDataDictionary)
                    );
        }

        public void SeedData(IFreeSqlContext context, AppConfig appConfig)
        {
            if (!appConfig.Zero.EnableSeedData)
                return;

            context.GetSet().Insert(SeedMenu.SeedMenus()).ExecuteAffrows();
            context.GetSet().Insert(SeedOperate.SeedOperates()).ExecuteAffrows();
            context.GetSet().Insert(SeedRole.SeedRoles()).ExecuteAffrows();
            context.GetSet().Insert(SeedRoleMenu.SeedRoleMenus()).ExecuteAffrows();
            context.GetSet().Insert(SeedSysUser.SeedSysUsers()).ExecuteAffrows();
        }
    }
}
