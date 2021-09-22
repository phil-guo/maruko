//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：08/22/2021  
//版本1.0
//===================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Maruko.Zero
{
    public class SysDataDictionaryService : CurdAppService<SysDataDictionary, SysDataDictionaryDTO>,
        ISysDataDictionaryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SysDataDictionaryService(IObjectMapper objectMapper, IFreeSqlRepository<SysDataDictionary> repository,
            IHttpContextAccessor httpContextAccessor) :
            base(objectMapper, repository)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void BeforeEdit(SysDataDictionaryDTO request)
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(claimsIdentity?.FindFirst(ClaimTypes.Sid)?.Value);
            if (request.IsBasicData && userId != 1)
                throw new Exception("非超级管理员不能修改基础数据");
        }

        public override void Delete(long id)
        {
            var entity = FirstOrDefault(id);
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt64(claimsIdentity?.FindFirst(ClaimTypes.Sid)?.Value);
            if (entity.IsBasicData && userId != 1)
                throw new Exception("非超级管理员不能删除基础数据");
            base.Delete(id);
        }

        public AjaxResponse<object> GetDictionaryByGroup(string groupName)
        {
            var data = Table
                .GetAll()
                .Select<SysDataDictionary>()
                .Where(item => item.Group == groupName)
                .ToList(_ => new
                {
                    _.Key,
                    _.Value
                });
            return new AjaxResponse<object>(data);
        }
    }
}