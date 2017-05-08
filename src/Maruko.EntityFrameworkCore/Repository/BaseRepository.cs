using System;
using System.Linq;
using Maruko.Domain.Entities;
using Maruko.Domain.Repositories;
using Maruko.Domain.UnitOfWork;
using Maruko.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Maruko.EntityFrameworkCore.Repository
{
    /// <summary>
    /// ef core 仓储具体实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : MarukoRepositoryBase<TEntity, long>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IDataBaseUnitOfWork _unitOfWork;

        private readonly ContextType _contextType;

        public BaseRepository(IDataBaseUnitOfWork unitOfWork, ContextType contextType)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork is null");

            _unitOfWork = unitOfWork;
            _contextType = contextType;
        }

        public BaseRepository(IDataBaseUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork is null");

            _unitOfWork = unitOfWork;
            _contextType = AttributeExtension.GetContextAttributeValue<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        #region IReposotpry

        public override IQueryable<TEntity> GetAll()
        {
            return GetSet();
        }

        public override TEntity Insert(TEntity entity)
        {
            try
            {
                GetSet().Add(entity);
                return entity;
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
                _unitOfWork.SetModify(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("ef core modify error:" + e.Message);
            }
        }


        public override void Delete(long id)
        {
            var entity = FirstOrDefault(id);

            if (entity == null)
                return;
            Delete(entity);
        }

        public override void Delete(TEntity entity)
        {
            GetSet().Remove(entity);
        }

        #endregion

        #region ISql

        public int ExecuteCommand(ContextType contextType, string sqlCommand, params object[] parameters)
        {
            return _unitOfWork.ExecuteCommand(sqlCommand, contextType, parameters);
        }

        #endregion

        #region Private Methods

        protected DbSet<TEntity> _entities;

        /// <summary>
        /// 创建上下文的实体对象
        /// </summary>
        /// <returns></returns>
        protected virtual DbSet<TEntity> GetSet()
        {
            return _entities ?? (_entities = _unitOfWork.CreateSet<TEntity>(_contextType));
        }

        #endregion
    }


}