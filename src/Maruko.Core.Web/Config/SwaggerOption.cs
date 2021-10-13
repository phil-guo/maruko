using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Web.Config
{
    public class SwaggerOption
    {
        public bool IsEnableSwagger { get; set; }
        public string Title { get; set; } = "sample";
        public string Version { get; set; } = "v1";
    }
}
