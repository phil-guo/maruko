using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Zero
{
    [AutoMap(typeof(SysUser))]
    public class SysUserDTO : EntityDto
    {
        public long RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Icon { get; set; }
    }
}