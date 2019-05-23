using Maruko.Modules;

namespace Maruko.MongoDB
{
    //[LoadOn(true, "Maruko.MongoDB")]
    public class MongoDbModule : MarukoModule
    {
        public override double Order { get; set; } = 0.6;
    }
}
