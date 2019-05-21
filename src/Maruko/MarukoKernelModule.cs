using Autofac;
using Autofac.Core;
using Maruko.Modules;
using Maruko.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko
{
    //[LoadOn(true, nameof(Maruko))]
    public class MarukoKernelModule : MarukoModule
    {
        public override double Order { get; set; } = 1;
    }
}
