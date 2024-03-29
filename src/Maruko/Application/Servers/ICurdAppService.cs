﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Domain.Entities;

namespace Maruko.Core.Application.Servers
{
    public interface ICurdAppServiceBase<TEntity, in TPrimaryKey, out TEntityDto, in TCreateEntityDto, in TUpdateEntityDto>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateEntityDto : EntityDto<TPrimaryKey>
        where TEntityDto : EntityDto<TPrimaryKey>
    {
        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TEntityDto Insert(TCreateEntityDto dto);

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        TEntityDto Update(TUpdateEntityDto dto);

        /// <summary>
        ///     物理删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKey id);

        /// <summary>
        ///     根据id 获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        ///     根据条件获取一个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    }

    public interface ICurdAppServiceBase<TEntity, TEntityDto, TCreateEntityDto, in TUpdateEntityDto>
        : ICurdAppServiceBase<TEntity, long, TEntityDto, TCreateEntityDto, TUpdateEntityDto>
        where TEntity : class, IEntity<long>
        where TUpdateEntityDto : EntityDto<long>
        where TEntityDto : EntityDto<long>
    {
        /// <summary>
        /// 批量提交
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        bool BatchInsert(List<TCreateEntityDto> dtos);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        bool BatchUpdate(List<TEntityDto> dtos);
    }
}