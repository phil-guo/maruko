using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Maruko.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Maruko.Hangfire
{
    //[LoadOn(true, "Maruko.Hangfire")]
    public class HangfireModule : MarukoModule
    {
        public override double Order { get; set; } = 0.4;
    }

}