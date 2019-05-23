using Autofac;
using Maruko.Modules;

namespace Maruko
{
    public class MarukoKernelModule : MarukoModule
    {
        public override double Order { get; set; } = 0.1;
        
        protected override void RegisterCustomModule(ContainerBuilder builder)
        {
            //new EventBusInstaller().Install();
        }
    }
}
