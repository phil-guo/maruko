using System.Collections.Generic;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface ISysMenuService : ICurdAppService<SysMenu, SysMenuDTO>
    {
        public List<MenusRoleResponse> GetMenusByRole(MenusRoleRequest request);

        public RoleMenuResponse GetMenusSetRole(MenusRoleRequest request);
    }
}