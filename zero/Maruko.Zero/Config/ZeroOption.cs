using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.Zero.Config
{
    public class ZeroOption
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public int AuthExpired { get; set; }
        public bool EnableDatabaseMigrate { get; set; } = false;
        public bool EnableSeedData { get; set; } = false;
    }
}
