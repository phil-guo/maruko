using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface ISysUserServices : ICurdAppService<SysUser, SysUserDTO>
    {
        AjaxResponse<object> ResetPassword(ResetPasswordRequest request);
        AjaxResponse<object> Login(LoginVM request);
        AjaxResponse<object> UpdatePersonalInfo(UpdatePersonalInfoRequest request);
    }
}