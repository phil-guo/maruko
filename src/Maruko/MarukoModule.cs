using System;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Modules;

namespace Maruko.Core
{
    public class MarukoModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterBuildCallback(lifetimeScopes =>
            {
                var provider = lifetimeScopes.Resolve<IServiceProvider>();
                ServiceLocator.Current = lifetimeScopes;
            });
        }
    }
}
