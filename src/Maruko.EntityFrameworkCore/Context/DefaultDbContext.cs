using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Configuration;
using Microsoft.Extensions.Configuration;

namespace Maruko.EntityFrameworkCore.Context
{
    public class DefaultDbContext : BaseDbContext
    {
        public DefaultDbContext()
        {
            GetConnStr();
        }


        private void GetConnStr()
        {
            ConnStr = ConfigurationSetting.DefaultConfiguration.GetConnectionString("DefaultConnection");
        }
    }
}
