using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Domain.Entities;

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
    }
}