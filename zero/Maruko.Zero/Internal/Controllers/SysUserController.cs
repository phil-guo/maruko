﻿using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Maruko.Zero
{
    [EnableCors("cors")]
    [Route("api/v1/sysUsers/")]
    public class SysUserController : BaseCurdController<SysUser, SysUserDTO>
    {
        private readonly ISysUserService _user;

        [HttpPost("updatePersonalInfo")]
        public AjaxResponse<object> UpdatePersonalInfo(UpdatePersonalInfoRequest request)
        {
            return _user.UpdatePersonalInfo(request);
        }

        [HttpPost("auth/token")]
        [AllowAnonymous]
        public AjaxResponse<object> Login(LoginVM request)
        {
            return _user.Login(request);
        }

        [HttpPost("resetPassword")]
        public AjaxResponse<object> ResetPassword(ResetPasswordRequest request)
        {
            return _user.ResetPassword(request);
        }

        public SysUserController(ICurdAppService<SysUser, SysUserDTO> curd, ISysUserService user) : base(curd)
        {
            _user = user;
        }
    }
}