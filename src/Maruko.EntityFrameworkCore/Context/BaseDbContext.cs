using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Maruko.Domain.Entities.Auditing;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace Maruko.EntityFrameworkCore.Context
{

    public abstract class BaseDbContext : DbContext
    {
        private readonly ConcurrentDictionary<string, object> _allSet = new ConcurrentDictionary<string, object>();

        public virtual string ConnStr { get; set; }


        public virtual DbSet<TEntity> CreateSet<TEntity, TPrimaryKey>()
            where TEntity : FullAuditedEntity<TPrimaryKey>
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
            optionsBuilder.UseMySQL(ConnStr);
        }
    }
}
