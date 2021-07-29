using Autofac;
using Maruko.Core.Modules;

namespace Maruko.Core
{
    public class MarukoModule : KernelModule
    {
        protected override void RegisterModule(ContainerBuilder builder)
        {
            base.RegisterModule(builder);
        }
    }
}
