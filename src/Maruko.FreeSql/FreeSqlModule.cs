using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Context;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.Modules;

namespace Maruko.Core.FreeSql
{
    public class FreeSqlModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterType<FreeSqlContext>().As<IFreeSqlContext>().SingleInstance();
            builder.RegisterGeneric(typeof(FreeSqlRepository<>)).As(typeof(IFreeSqlRepository<>))
                .InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(CurdAppService<,,>)).As(typeof(ICurdAppService<,,>));
            
            builder.RegisterAssemblyTypes(ContainerBuilderExtensions.ReferenceAssembly.ToArray())
                .Where(item => item.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

        }
    }
}
