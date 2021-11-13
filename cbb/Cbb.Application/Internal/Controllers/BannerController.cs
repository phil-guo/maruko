using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cbb.Application
{
    [EnableCors("cors")]
    [Route("api/v1/banners/")]
    public class BannerController : BaseCurdController<Banner, BannerDTO>
    {
        public BannerController(ICurdAppService<Banner, BannerDTO> curd) : base(curd)
        {
        }
    }
}