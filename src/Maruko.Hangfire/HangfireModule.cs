using Maruko.Modules;

namespace Maruko.Hangfire
{
    public class HangfireModule : MarukoModule
    {
        public override double Order { get; set; } = 0.5;
    }

}