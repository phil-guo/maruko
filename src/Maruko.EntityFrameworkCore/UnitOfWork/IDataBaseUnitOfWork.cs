using System;
using Maruko.Dependency;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Maruko.EntityFrameworkCore.UnitOfWork
{
    /// <summary>
    /// 创建上下文当中的实体的扩展工作单元
    /// </summary>
    public interface IEfUnitOfWork : IUnitOfWork, ISql, IDependencyTransient
    {
        /// <summary>
        ///     返回在上下文中访问给定类型的实体的IDbSet实例，对象管理状态和底层存储
        ///     Returns a IDbSet instance for access to entities of the given type in the context,
        ///     the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="TPrimaryKey">主键</typeparam>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity, TPrimaryKey>(ContextType contextType)
            where TEntity : FullAuditedEntity<TPrimaryKey>;

        DbSet<TEntity> WriteCreateSet<TEntity, TPrimaryKey>(ContextType contextType)
            where TEntity : FullAuditedEntity<TPrimaryKey>;

        /// <summary>
        ///     设置更改
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="TPrimaryKey">主键</typeparam>
        /// <param name="entity"></param>
        void SetModify<TEntity, TPrimaryKey>(TEntity entity) where TEntity : FullAuditedEntity<TPrimaryKey>;

        void SetModify<TEntity, TPrimaryKey>(TEntity entity, string[] inCludeColums) where TEntity : FullAuditedEntity<TPrimaryKey>;
    }
}