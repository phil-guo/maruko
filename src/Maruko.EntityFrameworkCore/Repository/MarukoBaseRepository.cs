using System;
using System.Linq;
using Maruko.Domain.Entities;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Maruko.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Maruko.EntityFrameworkCore.Repository
{
    /// <summary>
    /// ef core 仓储具体实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class MarukoBaseRepository<TEntity, TPrimaryKey> : MarukoRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>
    {
        protected readonly IEfUnitOfWork _unitOfWork;

        private readonly ContextType _contextType;

        protected MarukoBaseRepository(IEfUnitOfWork unitOfWork, ContextType contextType)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork is null");

            UnitOfWork = unitOfWork;
            _unitOfWork = unitOfWork;
            _contextType = contextType;
        }

        protected MarukoBaseRepository(IEfUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork is null");

            UnitOfWork = unitOfWork;
            _unitOfWork = unitOfWork;
            _contextType = AttributeExtension.GetContextAttributeValue<TEntity>();
        }

        #region IReposotpry

        public override IQueryable<TEntity> GetAll(bool isMaster = false)
        {
            if (isMaster)
                return WriteGetSet();
            return GetSet();
        }

        public override TEntity Insert(TEntity entity)
        {
            try
            {
                WriteGetSet().Add(entity);
                return _unitOfWork.Commit() <= 0 ? null : entity;
            }
            catch (Exception e)
            {
                throw new Exception("ef core add error:" + e.Message);
            }
        }

        public override TEntity Update(TEntity entity)
        {
            try
            {
                _unitOfWork.SetModify<TEntity, TPrimaryKey>(entity);
                return _unitOfWork.Commit() <= 0 ? null : entity;
            }
            catch (Exception e)
            {
                throw new Exception("ef core modify error:" + e.Message);
            }
        }

        public override TEntity UpdateColumn(TEntity entity, Func<TEntity, string[]> funcColums)
        {
            try
            {
                _unitOfWork.SetModify<TEntity, TPrimaryKey>(entity, funcColums(entity));
                return _unitOfWork.Commit() <= 0 ? null : entity;
            }
            catch (Exception e)
            {
                throw new Exception("ef core modify error:" + e.Message);
            }
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);

            if (entity == null)
                return;
            Delete(entity);
        }

        public override void Delete(TEntity entity)
        {
            WriteGetSet().Remove(entity);
            _unitOfWork.Commit();
        }

        #endregion

        #region ISql

        public int ExecuteCommand(ContextType contextType, string sqlCommand, params object[] parameters)
        {
            return _unitOfWork.ExecuteCommand(sqlCommand, contextType, parameters);
        }

        #endregion

        #region Private Methods

        private DbSet<TEntity> _entities;
        private DbSet<TEntity> _writeEntities;

        /// <summary>
        /// 创建上下文的实体对象
        /// </summary>
        /// <returns></returns>
        protected virtual DbSet<TEntity> GetSet()
        {
            return _entities ?? (_entities = _unitOfWork.CreateSet<TEntity, TPrimaryKey>(_contextType));
        }

        protected virtual DbSet<TEntity> WriteGetSet()
        {
            return _writeEntities ?? (_writeEntities = _unitOfWork.WriteCreateSet<TEntity, TPrimaryKey>(_contextType));
        }

        #endregion
    }
}