using Autofac;
using Maruko.Core.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.EntityFrameworkCore
{
    //[LoadOn(true, @"Maruko.EntityFrameworkCore")]
    public class EntityFrameworkCoreModule : KernelModule
    {
        public override double Order { get; set; } = 0.4;
    }
}
