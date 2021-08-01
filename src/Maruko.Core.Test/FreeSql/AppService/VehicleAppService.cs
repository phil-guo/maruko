using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;
using Maruko.Core.Test.FreeSql.DTO;
using Maruko.Core.Test.FreeSql.Model;

namespace Maruko.Core.Test.FreeSql.AppService
{
    public class VehicleAppService : CurdAppService<Vehicle, VehicleDTO>, IVehicleAppService
    {
        public VehicleAppService(IObjectMapper objectMapper, IFreeSqlRepository<Vehicle> repository) 
            : base(objectMapper, repository)
        {
        }
    }
}
