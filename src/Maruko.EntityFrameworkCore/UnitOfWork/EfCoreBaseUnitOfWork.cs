using System;
using System.Linq;
using log4net;
using Maruko.Domain.Entities;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.Logger;
using Microsoft.EntityFrameworkCore;

namespace Maruko.EntityFrameworkCore.UnitOfWork
{
    /// <summary>
    /// ef core 的工作单元具体实现
    /// </summary>
    public abstract class EfCoreBaseUnitOfWork<TContext> : IDataBaseUnitOfWork
        where TContext : DbContext
    {
        public TContext DefaultDbContext;

        public ILog Logger { get; }

        protected EfCoreBaseUnitOfWork()
        {
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(EfCoreBaseUnitOfWork<>));
        }

        public void Dispose()
        {
            if (DefaultDbContext != null)
                DefaultDbContext.Dispose();
        }

        public void Commit()
        {
            try
            {
                //这里如果没有调用过createset方法就会报错，如果没有调用认为没有任何改变直接跳出来
                if (DefaultDbContext == null)
                    return;
                DefaultDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Logger.Debug("DbUpdateException:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;
            do
            {
                try
                {
                    DefaultDbContext.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList()
                        .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
                catch (DbUpdateException ex)
                {
                    Logger.Debug("DbUpdateException:" + ex.Message);
                    throw new Exception(ex.Message);
                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker
            // as 'unchanged state'
            if (DefaultDbContext != null)
                DefaultDbContext.ChangeTracker.Entries()
                    .ToList()
                    .ForEach(entry =>
                    {
                        if (entry.State != EntityState.Unchanged)
                            entry.State = EntityState.Detached;
                    });


        }

        public virtual DbSet<TEntity> CreateSet<TEntity, TPrimaryKey>(ContextType contextType) where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }


        public void SetModify<TEntity, TPrimaryKey>(TEntity entity) where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            if (DefaultDbContext != null)
                DefaultDbContext.Entry(entity).State = EntityState.Modified;
        }

        #region Isql
        public int ExecuteCommand(string sqlCommand, ContextType contextType = ContextType.DefaultContextType, params object[] parameters)
        {
            switch (contextType)
            {
                default:
                    return GeneralDbContext(sqlCommand, parameters);
            }
        }

        private int GeneralDbContext(string sqlCommand, params object[] parameters)
        {
            if (DefaultDbContext != null)
                return DefaultDbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
            Logger.Debug($"DbContext 上下文创建失败");
            return -1;
        }

        #endregion
    }
}