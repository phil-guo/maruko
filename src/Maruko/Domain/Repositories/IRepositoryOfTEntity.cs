using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Core.Domain.Entities;

namespace Maruko.Core.Domain.Repositories
{
    /// <summary>
    ///     A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}" /> for most used primary key type (<see cref="Guid" />).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : class, IEntity<long>
    {
        

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