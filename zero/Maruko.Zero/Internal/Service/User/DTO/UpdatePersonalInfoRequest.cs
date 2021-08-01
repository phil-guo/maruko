using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Zero
{
    public class UpdatePersonalInfoRequest
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Icon { get; set; }
    }
}
