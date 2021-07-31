using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Core.Domain.Entities;
using Maruko.Core.FreeSql.Internal.Context;

namespace Maruko.Core.FreeSql.Internal.Repos
{
    public class FreeSqlRepository<TEntity> : IFreeSqlRepository<TEntity>
        where TEntity : FreeSqlEntity
    {
        private readonly IFreeSqlContext _context;

        public FreeSqlRepository(IFreeSqlContext context)
        {
            _context = context;
        }

        public TEntity Insert(TEntity entity)
        {
            return GetAll().GetRepository<TEntity>().Insert(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var repos = GetAll().Update<TEntity>().SetSource(entity).ExecuteAffrows();
            return repos > 0 ? entity : null;
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().Select<TEntity>().ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Select<TEntity>().Where(predicate).ToList();
        }

        public TEntity FirstOrDefault(long id)
        {
            return GetAll().Select<TEntity>().Where(CreateEqualityExpressionForId(id)).ToOne();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Select<TEntity>().Where(predicate).ToOne();
        }

        public void Delete(long id, bool isPhysics = false)
        {
            if (isPhysics)
                GetAll().Delete<TEntity>().Where(CreateEqualityExpressionForId(id)).ExecuteAffrows();
            else
            {
                GetAll().Update<TEntity>().Set(item => item.IsDelete, true)
                    .Where(item => item.Id == id)
                    .ExecuteAffrows();
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate, bool isPhysics = false)
        {
            if (isPhysics)
                GetAll().Delete<TEntity>().Where(predicate).ExecuteAffrows();
            else
            {
                GetAll().Update<TEntity>().Set(item => item.IsDelete, true)
                    .Where(predicate)
                    .ExecuteAffrows();
            }
        }

        public int Count()
        {
            return Convert.ToInt32(GetAll().Select<TEntity>().Count());
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Convert.ToInt32(GetAll().Select<TEntity>().Where(predicate).Count());
        }

        public bool BatchInsert(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool BatchUpdate(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(long id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(long))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public IFreeSql GetAll()
        {
            return _context.GetSet();
        }
    }
}
