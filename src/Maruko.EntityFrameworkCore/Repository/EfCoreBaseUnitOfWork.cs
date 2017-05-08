using System;
using System.Linq;
using Maruko.Domain.Entities;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Maruko.EntityFrameworkCore.Repository
{
    /// <summary>
    /// ef core 的工作单元具体实现
    /// </summary>
    public abstract class EfCoreBaseUnitOfWork : IDataBaseUnitOfWork
    {
        public DefaultDbContext _defaultDbContext;

        public void Dispose()
        {
            if (_defaultDbContext != null)
                _defaultDbContext.Dispose();
        }

        public void Commit()
        {
            try
            {
                //这里如果没有调用过createset方法就会报错，如果没有调用认为没有任何改变直接跳出来
                if (_defaultDbContext == null)
                    return;
                _defaultDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
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
                    throw new Exception(ex.Message);
                }
            } while (saveFailed);
        }

        public void RollbackChanges()
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

        public abstract DbSet<TEntity> CreateSet<TEntity>(ContextType contextType) where TEntity : class, IEntity;


        public void SetModify<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (_defaultDbContext != null)
                _defaultDbContext.Entry(entity).State = EntityState.Modified;
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
            if (_defaultDbContext == null)
                return -1;
            return _defaultDbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion


    }
}