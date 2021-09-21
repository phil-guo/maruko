using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cbb.Application
{
    [EnableCors("cors")]
    [Route("api/v1/appcarbrands/")]
    public class AppCarBrandController : BaseCurdController<AppCarBrand, AppCarBrandDTO>
    {
        public AppCarBrandController(ICurdAppService<AppCarBrand, AppCarBrandDTO> curd) : base(curd)
        {
        }
    }
}