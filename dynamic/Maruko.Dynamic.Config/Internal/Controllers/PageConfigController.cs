using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Dynamic.Config
{
    [EnableCors("cors")]
    [Route("api/v1/pageConfigs/")]
    public class PageConfigController : BaseCurdController<PageConfig, PageConfigDTO>
    {
        public PageConfigController(ICurdAppService<PageConfig, PageConfigDTO> curd) : base(curd)
        {
        }
    }
}
