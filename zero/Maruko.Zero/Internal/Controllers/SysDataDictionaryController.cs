using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Zero
{
    [EnableCors("cors")]
    [Route("api/v1/sysDataDictionaries/")]
    public class SysDataDictionaryController : BaseCurdController<SysDataDictionary, SysDataDictionaryDTO>
    {
        public SysDataDictionaryController(ICurdAppService<SysDataDictionary, SysDataDictionaryDTO> curd) : base(curd)
        {
        }
    }
}