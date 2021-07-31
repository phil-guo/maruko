using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.AutoMapper.AutoMapper;

namespace Maruko.Core.Test.AutoMap.Model
{
    [AutoMap(typeof(ModelA))]
    public class ModelB
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
