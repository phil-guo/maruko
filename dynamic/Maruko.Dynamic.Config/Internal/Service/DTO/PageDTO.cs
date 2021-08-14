using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Dynamic.Config
{
    [AutoMap(typeof(Page))]
    public class PageDTO : EntityDto
    {
        public string Name { get; set; }
        public string Key { get; set; }
    }
}
