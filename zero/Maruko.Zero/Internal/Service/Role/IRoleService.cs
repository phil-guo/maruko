
using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface IRoleService : ICurdAppService<SysRole, RoleDTO>
    {
        AjaxResponse<object> GetAllRoles();
        public bool SetRolePermission(SetRolePermissionRequest request);
    }
}