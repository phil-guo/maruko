using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Maruko.Configuration;
using Microsoft.Extensions.Configuration;

namespace Maruko.EntityFrameworkCore.Context
{
    public class DefaultDbContext : BaseDbContext
    {
        public override string ConnStr { get; set; } = ConfigurationSetting.DefaultConfiguration.GetConnectionString("DefaultConnection");


    }
}
