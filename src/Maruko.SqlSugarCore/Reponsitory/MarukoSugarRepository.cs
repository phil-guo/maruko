using System;
using System.Linq;
using System.Linq.Expressions;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.SqlSugarCore.UnitOfWork;
using SqlSugar;

namespace Maruko.SqlSugarCore.Reponsitory
{
    public abstract class MarukoSugarRepository<TEntity, TPrimaryKey> : MarukoRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>, new()
    {
        private readonly ISugarUnitOfWork _unitOfWork;

        public MarukoSugarRepository(ISugarUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ISugarUnitOfWork SugarUnitOfWork => _unitOfWork;


        #region select/query

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return SugarQuery().Single(predicate);
        }

        #endregion

        public override TEntity Insert(TEntity entity)
        {
            return GetSet().Insertable(entity).ExecuteReturnEntity();
        }

        public override TEntity Update(TEntity entity)
        {
            return GetSet().Updateable(entity).ExecuteCommand() > 0 ? entity : null;
        }

        public override void Delete(TPrimaryKey id)
        {
            GetSet().Deleteable<TEntity>().In(id).ExecuteCommand();
        }

        public override void Delete(TEntity entity)
        {
            GetSet().Deleteable<TEntity>().Where(entity).ExecuteCommand();
        }

        public override IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }


        public ISugarQueryable<TEntity> SugarQuery()
        {
            return GetSet().Queryable<TEntity>();
        }

        private SqlSugarClient GetSet()
        {
            return _unitOfWork.SqlSugarClient();
        }
    }
}
