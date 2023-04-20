package com.act.modules.zero.application.services.sysUser;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.modules.zero.application.services.sysUser.dto.LoginDTO;
import com.act.modules.zero.application.services.sysUser.dto.SysUserDTO;
import com.act.modules.zero.domain.SysUser;
import com.act.modules.zero.mapper.SysUserMapper;
import org.apache.ibatis.annotations.Mapper;

public interface SysUserService extends ICurdAppService<SysUser, SysUserDTO> {

    /**
     * 登录
     *
     * @param request
     * @return
     * @throws InstantiationException
     * @throws IllegalAccessException
     */
    AjaxResponse<Object> Login(LoginDTO request) throws InstantiationException, IllegalAccessException;
}
