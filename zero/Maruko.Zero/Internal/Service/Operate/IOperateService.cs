using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface IOperateService : ICurdAppService<SysOperate, OperateDTO>
    {
        MenuOfOperateResponse GetMenuOfOperate(MenuOfOperateRequest request);
    }
}