using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Dynamic.Config
{
    [AutoMap(typeof(PageTableConfig))]
    public class PageTableConfigDTO : EntityDto
    {
        public long PageId { get; set; }
        public string DataUrl { get; set; }
        public bool IsRow { get; set; }
        public bool IsMultiple { get; set; }
        public int RowWith { get; set; }
        public string Fields { get; set; }
    }
}
