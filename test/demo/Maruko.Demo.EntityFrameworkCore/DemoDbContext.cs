using Maruko.Configuration;
using Maruko.Demo.EntityFrameworkCore.Config;
using Maruko.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Maruko.Demo.EntityFrameworkCore
{
    public class DemoDbContext : BaseDbContext
    {
        public override string ConnStr { get; set; } = ConfigurationSetting.DefaultConfiguration.GetConnectionString("DefaultConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.DeployUser();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnStr);
        }
    }
}
