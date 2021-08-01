using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;

namespace Maruko.Zero
{
    public interface ISysUserServices : ICurdAppService<SysUser, SysUserDTO>
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AjaxResponse<object> ResetPassword(ResetPasswordRequest request);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AjaxResponse<object> Login(LoginVM request);

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AjaxResponse<object> UpdatePersonalInfo(UpdatePersonalInfoRequest request);
    }
}