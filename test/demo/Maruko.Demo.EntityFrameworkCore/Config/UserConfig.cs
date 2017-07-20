using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Demo.Core;
using Microsoft.EntityFrameworkCore;

namespace Maruko.Demo.EntityFrameworkCore.Config
{
    public static class UserConfig
    {
        public static void DeployUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(item =>
            {
                item.ToTable($"sys_user");
            });
        }
    }
}
