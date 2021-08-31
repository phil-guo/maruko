using System;
using System.Collections.Generic;
using System.Text;
using Maruko.Core.Application;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.Domain.Entities;
using Maruko.Core.FreeSql.Internal;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web.Internal.VM;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Core.Web
{
    [ApiController]
    public abstract class BaseCurdController<TEntity, TEntityDto> : ControllerBase
        where TEntity : FreeSqlEntity
        where TEntityDto : EntityDto
    {
        private readonly ICurdAppService<TEntity, TEntityDto> _curd;

        protected BaseCurdController(ICurdAppService<TEntity, TEntityDto> curd)
        {
            _curd = curd;
        }

        /// <summary>
        ///     分页查询
        /// </summary>
        /// <param name="search"> 查询条件</param>
        /// <returns></returns>
        [HttpPost("pageSearch")]
        //[Authorize(Roles = "1,3")]//权限角色id
        public virtual AjaxResponse<PagedResultDto> PageSearch(PageDto search)
        {
            return new AjaxResponse<PagedResultDto>(_curd.PageSearch(search));
            ;
        }

        /// <summary>
        ///     创建或修改
        /// </summary>
        /// <param name="model"> 模型dto</param>
        /// <returns></returns>
        [HttpPost("createOrEdit")]
        //[Authorize(Roles = "1,3")]//权限角色id
        public virtual AjaxResponse<object> CreateOrUpdate(TEntityDto model)
        {
            return new AjaxResponse<object>(_curd.CreateOrEdit(model));
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id"> 主键</param>
        /// <returns></returns>
        [HttpPost("remove")]
        //[Authorize(Roles = "1,3")]//权限角色id
        public virtual AjaxResponse<object> Remove(int id)
        {
            try
            {
                _curd.Delete(id);
                return new AjaxResponse<object>("删除成功");
            }
            catch (Exception exception)
            {
                return new AjaxResponse<object>($"{exception.Message}", 500);
                ;
            }
        }

        [HttpPost("removes")]
        public virtual AjaxResponse<object> Remove(RemovesDTO ids)
        {
            try
            {
                ids.Ids.ForEach(id => { _curd.Delete(id); });
                return new AjaxResponse<object>("删除成功");
            }
            catch (Exception exception)
            {
                return new AjaxResponse<object>($"{exception.Message}", 500);
                ;
            }
        }
    }
}