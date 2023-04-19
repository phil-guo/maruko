package com.act.modules.zero.services.sysuser.imp;

import com.act.core.utils.AjaxResponse;
import com.act.core.utils.JWTUtils;
import com.act.modules.zero.mapper.SysUserMapper;
import com.act.modules.zero.services.sysuser.SysUserService;
import com.act.modules.zero.services.sysuser.dto.LoginDTO;
import lombok.var;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.util.Hashtable;

@Service
public class SysUserServiceImp implements SysUserService {

    @Resource
    private SysUserMapper sysUserMapper;

    public AjaxResponse<Object> Login(LoginDTO request) {

        var map = new Hashtable<String, Object>();
        map.put("userId", 1);
        var token = JWTUtils.getToken(map);
        return new AjaxResponse<Object>(token);
    }
}
