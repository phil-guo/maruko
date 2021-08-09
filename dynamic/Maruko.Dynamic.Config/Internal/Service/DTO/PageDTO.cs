using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Maruko.Core.Application.Servers.Dto;

namespace Maruko.Dynamic.Config
{
    [AutoMap(typeof(Page))]
    public class PageDTO : EntityDto
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string DataUrl { get; set; }
        public bool IsRow { get; set; }
        public bool IsMultiple { get; set; }
        public int RowWith { get; set; }
        public string DefaultQueryCondition { get; set; }
    }
}
