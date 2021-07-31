using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Application.Servers.Dto
{
    public class DynamicFilter
    {
        public string Field { get; set; }
        public string Operate { get; set; }
        public object Value { get; set; }
    }
}
