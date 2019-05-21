using Maruko.Modules;

namespace Maruko.AspNetMvc
{
    //[LoadOn(true, "Maruko.AspNetMvc")]
    public class AspNetMvcModule : MarukoModule
    {
        public override double Order { get; set; } = 2;
    }
}
