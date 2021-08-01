using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Zero
{
    [EnableCors("cors")]
    [Route("api/v1/sysOperate/")]
    public class SysOperateController : BaseCurdController<SysOperate, OperateDTO>
    {
        private readonly IOperateService _operate;

        [HttpPost("getMenuOfOperate")]
        public AjaxResponse<object> GetMenuOfOperate(MenuOfOperateRequest request)
        {
            return new AjaxResponse<object>(_operate.GetMenuOfOperate(request), "ok");
        }

        public SysOperateController(ICurdAppService<SysOperate, OperateDTO> curd, IOperateService operate) : base(curd)
        {
            _operate = operate;
        }
    }
}