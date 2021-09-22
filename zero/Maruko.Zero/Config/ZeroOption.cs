using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Zero.Config
{
    public class ZeroOption
    {
       
        public bool EnableDatabaseMigrate { get; set; } = false;
        public bool EnableSeedData { get; set; } = false;
    }
}
