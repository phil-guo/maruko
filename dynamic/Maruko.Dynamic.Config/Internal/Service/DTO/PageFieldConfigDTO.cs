using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Dynamic.Config
{
    [AutoMap(typeof(PageFieldConfig))]
    public class PageFieldConfigDTO : EntityDto
    {
        public long PageTableId { get; set; }
        public string Name { get; set; }
        public string Field { get; set; }
        public string Url { get; set; }
        public bool IsQuery { get; set; }
        public int Width { get; set; }
        public int MinWidth { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public string Condition { get; set; }
    }
}
