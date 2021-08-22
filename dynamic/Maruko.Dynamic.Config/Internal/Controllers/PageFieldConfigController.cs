using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Dynamic.Config
{
        [EnableCors("cors")]
        [Route("api/v1/pagefieldconfigs/")]
        public class PageFieldConfigController : BaseCurdController<PageFieldConfig, PageFieldConfigDTO>
        {
            public PageFieldConfigController(ICurdAppService<PageFieldConfig, PageFieldConfigDTO> curd) : base(curd)
            {
            }
        }
    }