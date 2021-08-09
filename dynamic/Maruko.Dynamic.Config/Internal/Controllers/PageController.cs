using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Dynamic.Config
{
    [EnableCors("cors")]
    [Route("api/v1/pages/")]
    public class PageController : BaseCurdController<Page, PageDTO>
    {
        public PageController(ICurdAppService<Page, PageDTO> curd) : base(curd)
        {
        }
    }
}
