using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Zero
{
    [EnableCors("cors")]
    [Route("api/v1/sysMenus/")]
    public class SysMenuController : BaseCurdController<SysMenu, SysMenuDTO>
    {
        private readonly ISysMenuService _menu;

        [HttpGet("getAllParentMenus")]
        public AjaxResponse<object> GetAllParentMenus()
        {
            return _menu.GetAllParentMenus();
        }

        [HttpPost("getMenusByRole")]
        public AjaxResponse<object> GetMenusByRole(MenusRoleRequest request)
        {
            return new AjaxResponse<object>(_menu.GetMenusByRole(request), "ok");
        }

        [HttpPost("getMenusSetRole")]
        public AjaxResponse<object> GetMenusSetRole(MenusRoleRequest request)
        {
            return new AjaxResponse<object>(_menu.GetMenusSetRole(request), "ok");
        }

        public SysMenuController(ICurdAppService<SysMenu, SysMenuDTO> curd, ISysMenuService menu) : base(curd)
        {
            _menu = menu;
        }
    }
}