using System.Collections.Generic;
using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface ISysMenuService : ICurdAppService<SysMenu, SysMenuDTO>
    {
        AjaxResponse<object> GetMenuOfOperate(long id);
        AjaxResponse<object> GetAllParentMenus();
        public List<MenusRoleResponse> GetMenusByRole(MenusRoleRequest request);

        public RoleMenuResponse GetMenusSetRole(MenusRoleRequest request);
    }
}