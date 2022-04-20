using System.Collections.Generic;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Zero
{
    [AutoMap(typeof(SysMenu))]
    public class SysMenuDTO : EntityDto
    {
        public int ParentId { get; set; } = 0;
        public string Name { get; set; }
        public string Url { get; set; }
        public int Level { get; set; } = 1;
        public string Operates { get; set; }
        public string Icon { get; set; }
        public string Key { get; set; } = "";
        public bool IsLeftShow { get; set; }
        public List<OperateModel> OperateModels { get; set; } = new List<OperateModel>();
        public List<SysMenuDTO> Children { get; set; } = new List<SysMenuDTO>();
    }

    public class OperateModel : EntityDto<int>
    {
        public string Name { get; set; }
    }
}