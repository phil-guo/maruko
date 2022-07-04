using Cbb.Application.Internal.Domain;
using Cbb.Application.Internal.Services.DTO;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cbb.Application.Internal.Controllers
{
    [EnableCors("cors")]
    [Route("api/v1/appOilTimes/")]
    public class AppOilTimeController : BaseCurdController<AppOilTime, AppOilTimeDTO>
    {
        public AppOilTimeController(ICurdAppService<AppOilTime, AppOilTimeDTO> curd) : base(curd)
        {
        }
    }
}
