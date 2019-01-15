using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Maruko.Dependency;
using Maruko.Domain.Entities;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;

namespace Maruko.Domain.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>
    {
        IUnitOfWork UnitOfWork { get; set; }

        #region Insert
        /// <summary>
        ///     Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        TEntity Insert(TEntity entity);

        #endregion

        #region Update

        /// <summary>
        ///     Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="funcColums"></param>
        /// <returns></returns>
        TEntity UpdateColumn(TEntity entity, Func<TEntity, string[]> funcColums);
        #endregion

        #region Select/Get/Query

        /// <summary>
        ///     Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> GetAll(bool isMaster = false);

        /// <summary>
        ///     Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        List<TEntity> GetAllList();

        /// <summary>
        ///     Used to get all entities based on given <paramref name="predicate" />.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        ///     Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Single an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Delete

        /// <summary>
        ///     Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        ///     Deletes many entities by function.
        ///     Notice that: All entities fits to given predicate are retrieved and deleted.
        ///     This may cause major performance problems if there are too many entities with
        ///     given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates

        /// <summary>
        ///     Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        int Count();

        /// <summary>
        ///     Gets count of all entities in this repository based on given <paramref name="predicate" />.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}