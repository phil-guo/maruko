using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Dynamic.Config
{
    [AutoMap(typeof(PageConfig))]
    public class PageConfigDTO : EntityDto
    {
        public long PageId { get; set; }
        public string Key { get; set; }
        public string DataUrl { get; set; }
        public bool IsRow { get; set; }
        public bool IsMultiple { get; set; }
        public int RowWith { get; set; }
        public string Fields { get; set; } = "";
        public string Buttons { get; set; } = "";
        public string Functions { get; set; } = "";
        public bool IsTableOperateRow { get; set; }
        public bool IsTableCheckRow { get; set; }
    }
}
