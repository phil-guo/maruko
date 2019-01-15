using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Dependency;
using Maruko.Domain.Entities;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;

namespace Maruko.Domain.Repositories
{
    /// <summary>
    ///     A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}" /> for most used primary key type (<see cref="Guid" />).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="total">总页数</param>
        /// <param name="orderSelector"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageMax">当前一页显示条数</param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> PageSearch(out int total,
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, int>> orderSelector = null,
            int pageIndex = 1,
            int pageMax = 20);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="total">总页数</param>
        /// <param name="orderSelector"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageMax">当前一页显示条数</param>
        /// <param name="selector">指定查询字段并返回</param>
        /// <param name="predicate">查询条件</param>
        List<dynamic> PageSearchSelector(out int total,
            Expression<Func<TEntity, dynamic>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, int>> orderSelector = null,
            int pageIndex = 1,
            int pageMax = 20);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool BatchInsert(List<TEntity> entities);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool BatchUpdate(List<TEntity> entities);

        /// <summary>
        /// 更新但不提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity UpdateNotCommit(TEntity entity);

        /// <summary>
        /// 添加但不提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity InsertNotCommit(TEntity entity);
    }
}