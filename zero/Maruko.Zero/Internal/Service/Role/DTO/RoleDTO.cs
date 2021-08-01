using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Zero
{
    [AutoMap(typeof(SysRole))]
    public class RoleDTO : EntityDto
    {
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}