//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：08/22/2021  
//版本1.0
//===================================================================================


using System.Collections.Generic;
using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace Maruko.Zero
{
    public class SysDataDictionaryService : CurdAppService<SysDataDictionary, SysDataDictionaryDTO>,
        ISysDataDictionaryService
    {
        public SysDataDictionaryService(IObjectMapper objectMapper, IFreeSqlRepository<SysDataDictionary> repository) :
            base(objectMapper, repository)
        {
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