
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface IRoleService : ICurdAppService<SysRole, RoleDTO>
    {
        public bool SetRolePermission(SetRolePermissionRequest request);
    }
}