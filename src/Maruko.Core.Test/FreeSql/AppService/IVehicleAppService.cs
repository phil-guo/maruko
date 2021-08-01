using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Test.FreeSql.DTO;
using Maruko.Core.Test.FreeSql.Model;

namespace Maruko.Core.Test.FreeSql.AppService
{
    public interface IVehicleAppService : ICurdAppService<Vehicle, VehicleDTO>
    {
    }
}
