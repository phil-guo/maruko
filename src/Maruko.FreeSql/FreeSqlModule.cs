using System;
using Autofac;
using Maruko.Core.Application.Servers;
using Maruko.Core.Modules;
using Maruko.FreeSql.Internal.AppService;
using Maruko.FreeSql.Internal.Context;
using Maruko.FreeSql.Internal.Repos;

namespace Maruko.FreeSql
{
    public class FreeSqlModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterType<FreeSqlContext>().As<IFreeSqlContext>().SingleInstance();
            builder.RegisterGeneric(typeof(FreeSqlRepository<>)).As(typeof(IFreeSqlRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(CurdAppService<,,>)).As(typeof(ICurdAppService<,,>));
        }
    }
}
