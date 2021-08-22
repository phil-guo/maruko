using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Dynamic.Config
{
        [EnableCors("cors")]
        [Route("api/v1/pagetableconfigs/")]
        public class PageTableConfigController : BaseCurdController<PageTableConfig, PageTableConfigDTO>
        {
            public PageTableConfigController(ICurdAppService<PageTableConfig, PageTableConfigDTO> curd) : base(curd)
            {
            }
        }
    }