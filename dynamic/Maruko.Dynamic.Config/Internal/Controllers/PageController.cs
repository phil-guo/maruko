using Maruko.Core.Application;
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
        private readonly IPageDomainService _domain;
        public PageController(ICurdAppService<Page, PageDTO> curd, IPageDomainService domain) : base(curd)
        {
            _domain = domain;
        }

        [HttpGet("getPageDetail")]
        public AjaxResponse<GetPageDetailDTO> GetPageDetail(string key)
        {
            return new AjaxResponse<GetPageDetailDTO>(_domain.GetPageDetail(key));
        }
    }
}
