using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Domain.Entities;
using Maruko.Core.Domain.Repositories;
using Maruko.Core.ObjectMapping;

namespace Maruko.Core.Application.Servers
{
    /// <summary>
    ///     增删改查基础实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TCreateEntityDto"></typeparam>
    /// <typeparam name="TUpdateEntityDto"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    public abstract class CurdAppServiceTPrimaryKey<TEntity, TPrimaryKey, TEntityDto, TCreateEntityDto, TUpdateEntityDto> :
        CurdAppServiceBase<TEntity, TPrimaryKey, TEntityDto, TCreateEntityDto, TUpdateEntityDto>,
        ICurdAppServiceBase<TEntity, TPrimaryKey, TEntityDto, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateEntityDto : EntityDto<TPrimaryKey>
        where TEntityDto : EntityDto<TPrimaryKey>
    {
        public readonly IRepository<TEntity, TPrimaryKey> Repository;


        protected CurdAppServiceTPrimaryKey(IRepository<TEntity, TPrimaryKey> repository, IObjectMapper objectMapper)
        : base(objectMapper)
        {
            Repository = repository;
        }

        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        /// <returns>实体</returns>
        public virtual TEntityDto Insert(TCreateEntityDto dto)
        {
            var entity = MapCreateToEntity(dto);
            entity = Repository.Insert(entity);
            return entity == null ? null : MapToEntityDto(entity);
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        /// <returns>实体</returns>
        public virtual TEntityDto Update(TUpdateEntityDto dto)
        {
            var entity = FirstOrDefault(dto.Id);
            MapToEntity(dto, entity);
            entity = Repository.Update(entity);
            return entity == null ? null : MapToEntityDto(entity);
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id">主键</param>
        public virtual void Delete(TPrimaryKey id)
        {
            Repository.Delete(id);
        }

        /// <summary>
        ///     查询一个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Repository.FirstOrDefault(id);
        }

        /// <summary>
        ///     查询一个实体
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FirstOrDefault(predicate);
        }
    }

    public abstract class CurdAppServiceBase<TEntity, TEntityDto, TCreateEntityDto, TUpdateEntityDto> :
        CurdAppServiceBase<TEntity, long, TEntityDto, TCreateEntityDto, TUpdateEntityDto>, ICurdAppServiceBase<TEntity, TEntityDto, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : class, IEntity<long>
        where TUpdateEntityDto : EntityDto<long>
        where TEntityDto : EntityDto<long>
    {
        public readonly IRepository<TEntity> Repository;

        protected CurdAppServiceBase(IObjectMapper objectMapper, IRepository<TEntity> repository) : base(objectMapper)
        {
            Repository = repository;
        }

        public virtual void Delete(long id)
        {
            Repository.Delete(id);
        }

        public virtual TEntity FirstOrDefault(long id)
        {
            return Repository.FirstOrDefault(id);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FirstOrDefault(predicate);
        }

        public virtual TEntityDto Insert(TCreateEntityDto dto)
        {
            var entity = MapCreateToEntity(dto);
            entity = Repository.Insert(entity);
            return entity == null ? null : MapToEntityDto(entity);
        }

        public virtual TEntityDto Update(TUpdateEntityDto dto)
        {
            var entity = FirstOrDefault(dto.Id);
            MapToEntity(dto, entity);
            entity = Repository.Update(entity);
            return entity == null ? null : MapToEntityDto(entity);
        }

        public bool BatchInsert(List<TCreateEntityDto> dtos)
        {
            var entities = new List<TEntity>();
            dtos.ForEach(dto =>
            {
                entities.Add(MapCreateToEntity(dto));
            });
            return Repository.BatchInsert(entities);
        }

        public bool BatchUpdate(List<TEntityDto> dtos)
        {
            var entities = new List<TEntity>();
            dtos.ForEach(dto =>
            {
                entities.Add(MapToEntity(dto));
            });
            return Repository.BatchUpdate(entities);
        }
    }
}