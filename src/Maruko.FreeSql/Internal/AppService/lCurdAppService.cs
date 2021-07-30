using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Maruko.Core.Application.Servers;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Domain.Entities;

namespace Maruko.FreeSql.Internal.AppService
{
    public interface ICurdAppService<TEntity, TEntityDto, in TSearch> : ICurdAppServiceBase<TEntity, TEntityDto, TEntityDto, TEntityDto>
        where TEntity : Entity
        where TEntityDto : EntityDto
        where TSearch : PageDto
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        PagedResultDto PageSearch(TSearch search);

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        TEntityDto CreateOrEdit(TEntityDto request);
    }
}
