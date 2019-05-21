using System;
using System.Linq;
using log4net;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.Context;
using Maruko.Logger;
using Microsoft.EntityFrameworkCore;

namespace Maruko.EntityFrameworkCore.UnitOfWork
{
    /// <summary>
    /// ef core 的工作单元具体实现
    /// </summary>
    public abstract class EfCoreBaseUnitOfWork<TContext> : IEfUnitOfWork
        where TContext : BaseDbContext
    {
        public readonly TContext _defaultDbContext;

        public ILog Logger { get; }

        public EfCoreBaseUnitOfWork(TContext defaultDbContext)
        {
            _defaultDbContext = defaultDbContext;
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(EfCoreBaseUnitOfWork<>));
        }

        public virtual void Dispose()
        {
            if (_defaultDbContext != null)
                _defaultDbContext.Dispose();
        }

        public virtual int Commit()
        {
            try
            {
                //这里如果没有调用过createset方法就会报错，如果没有调用认为没有任何改变直接跳出来
                if (_defaultDbContext == null)
                    return 0;
                return _defaultDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Logger.Debug("DbUpdateException:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public virtual void CommitAndRefreshChanges()
        {
            bool saveFailed;
            do
            {
                try
                {
                    if (_defaultDbContext == null)
                        return;

                    _defaultDbContext.SaveChanges();
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

        public virtual void RollbackChanges()
        {
            // set all entities in change tracker
            // as 'unchanged state'
            if (_defaultDbContext != null)
                _defaultDbContext.ChangeTracker.Entries()
                    .ToList()
                    .ForEach(entry =>
                    {
                        if (entry.State != EntityState.Unchanged)
                            entry.State = EntityState.Detached;
                    });
        }
        
        public virtual void SetModify<TEntity, TPrimaryKey>(TEntity entity) where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            if (_defaultDbContext != null)
                _defaultDbContext.Entry(entity).State = EntityState.Modified;
        }

        #region Isql

        public virtual int ExecuteCommand(string sqlCommand, ContextType contextType = ContextType.DefaultContextType,
            params object[] parameters)
        {
            switch (contextType)
            {
                default:
                    return GeneralDbContext(sqlCommand, parameters);
            }
        }

        private int GeneralDbContext(string sqlCommand, params object[] parameters)
        {
            if (_defaultDbContext != null)
                return _defaultDbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
            Logger.Debug($"DbContext 上下文创建失败");
            return -1;
        }

        public DbSet<TEntity> CreateSet<TEntity, TPrimaryKey>()
            where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}