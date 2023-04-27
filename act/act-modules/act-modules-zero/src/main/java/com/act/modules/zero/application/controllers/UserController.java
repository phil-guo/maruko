package com.act.modules.zero.application.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.application.services.user.SysUserService;
import com.act.modules.zero.application.services.user.dto.LoginDTO;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@Api(tags = "系统用户模块")
@RestController
@RequestMapping("/api/v1/sysUsers/")
@SuppressWarnings("all")
public class UserController {

    @Autowired
    private SysUserService _user;

    @ApiOperation(value = "登录")
    @PostMapping("auth/token")
    public AjaxResponse<Object> login(@RequestBody LoginDTO request) throws InstantiationException, IllegalAccessException, FriendlyException {
        return _user.login(request);
    }
}
