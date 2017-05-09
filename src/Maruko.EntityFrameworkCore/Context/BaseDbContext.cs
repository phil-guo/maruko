using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Maruko.Configuration;
using Microsoft.Extensions.Configuration;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Maruko.EntityFrameworkCore.Context
{

    public abstract class BaseDbContext : DbContext
    {
        private readonly ConcurrentDictionary<string, object> _allSet = new ConcurrentDictionary<string, object>();

        public  string ConnStr { get; set; }


        public virtual DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            var key = typeof(TEntity).FullName;
            object result;

            if (!_allSet.TryGetValue(key, out result))
            {
                result = Set<TEntity>();
                _allSet.TryAdd(key, result);
            }
            return Set<TEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Data Source=192.168.26.203;port=3306;user id=root;password=qwe123QWE;database=bill;Charset=utf8;
            optionsBuilder.UseMySQL(ConnStr);
        }

        
    }
}
