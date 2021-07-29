using Maruko.Core.Modules;
using Maruko.Modules;

namespace Maruko.AspNetMvc
{
    //[LoadOn(true, "Maruko.AspNetMvc")]
    public class AspNetMvcModule : KernelModule
    {
        public override double Order { get; set; } = 0.2;
    }
}
