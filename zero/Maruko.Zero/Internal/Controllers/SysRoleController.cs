using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Zero
{
    [EnableCors("cors")]
    [Route("api/v1/sysRole/")]
    public class SysRoleController : BaseCurdController<SysRole, RoleDTO>
    {
        private readonly IRoleService _roleService;

        [HttpPost("setRolePermission")]
        public AjaxResponse<object> SetRolePermission(SetRolePermissionRequest request)
        {
            return new AjaxResponse<object>(_roleService.SetRolePermission(request), "ok");
        }

        public SysRoleController(ICurdAppService<SysRole, RoleDTO> curd, IRoleService roleService) : base(curd)
        {
            _roleService = roleService;
        }
    }
}