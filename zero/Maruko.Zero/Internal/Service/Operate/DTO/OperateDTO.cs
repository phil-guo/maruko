using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Zero
{
    [AutoMap(typeof(SysOperate))]
    public class OperateDTO : EntityDto
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public int Unique { get; set; }
    }
}