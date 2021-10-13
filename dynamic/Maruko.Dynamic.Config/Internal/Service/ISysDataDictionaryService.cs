using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Dynamic.Config
{
    public interface ISysDataDictionaryService : ICurdAppService<SysDataDictionary, SysDataDictionaryDTO>
    {
        AjaxResponse<object> GetDictionaryByGroup(string groupName);
    }
}