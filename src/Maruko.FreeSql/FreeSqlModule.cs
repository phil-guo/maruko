using System;
using Autofac;
using Maruko.Core.Modules;

namespace Maruko.FreeSql
{
    public class FreeSqlModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            base.RegisterModule(builder);
        }
    }
}
