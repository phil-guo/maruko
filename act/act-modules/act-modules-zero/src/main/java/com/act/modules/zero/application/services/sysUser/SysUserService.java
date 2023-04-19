package com.act.modules.zero.application.services.sysUser;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.modules.zero.application.services.sysUser.dto.LoginDTO;
import com.act.modules.zero.application.services.sysUser.dto.SysUserDTO;
import com.act.modules.zero.domain.SysUser;


public interface SysUserService extends ICurdAppService<SysUser, SysUserDTO> {

    //登录
    AjaxResponse<Object> Login(LoginDTO request) throws InstantiationException, IllegalAccessException;
}
