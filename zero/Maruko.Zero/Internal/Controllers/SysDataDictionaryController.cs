using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Zero
{
    [EnableCors("cors")]
    [Route("api/v1/sysDataDictionaries/")]
    public class SysDataDictionaryController : BaseCurdController<SysDataDictionary, SysDataDictionaryDTO>
    {
        private readonly ISysDataDictionaryService _dictionary;

        public SysDataDictionaryController(ICurdAppService<SysDataDictionary, SysDataDictionaryDTO> curd,
            ISysDataDictionaryService dictionary) : base(curd)
        {
            _dictionary = dictionary;
        }

        [HttpGet("getDictionaryByGroup")]
        public AjaxResponse<object> GetDictionaryByGroup(string groupName)
        {
            return _dictionary.GetDictionaryByGroup(groupName);
        }
    }
}