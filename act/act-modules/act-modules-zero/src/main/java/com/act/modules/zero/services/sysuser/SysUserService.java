package com.act.modules.zero.services.sysuser;

import com.act.core.utils.AjaxResponse;
import com.act.modules.zero.services.sysuser.dto.LoginDTO;


public interface SysUserService {

    //登录
    AjaxResponse<Object> Login(LoginDTO request);
}
