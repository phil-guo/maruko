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
        public string Fields { get; set; }
        public string Buttons { get; set; }
        public string Functions { get; set; }
    }
}
