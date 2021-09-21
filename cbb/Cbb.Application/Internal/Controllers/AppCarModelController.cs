using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cbb.Application
{
        [EnableCors("cors")]
        [Route("api/v1/appcarmodels/")]
        public class AppCarModelController : BaseCurdController<AppCarModel, AppCarModelDTO>
        {
            public AppCarModelController(ICurdAppService<AppCarModel, AppCarModelDTO> curd) : base(curd)
            {
            }
        }
    }