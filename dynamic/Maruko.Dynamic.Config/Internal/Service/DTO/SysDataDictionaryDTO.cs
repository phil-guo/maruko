using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Dynamic.Config
{
    [AutoMap(typeof(SysDataDictionary))]
    public class SysDataDictionaryDTO : EntityDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public bool IsBasicData { get; set; }
    }
}
