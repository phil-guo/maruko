using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Core.Web.Config
{
    public class WebOption
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public int AuthExpired { get; set; }
    }
}
