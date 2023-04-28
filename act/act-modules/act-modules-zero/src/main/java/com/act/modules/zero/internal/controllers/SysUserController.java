package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.user.SysUserService;
import com.act.modules.zero.internal.application.user.dto.LoginDTO;
import com.act.modules.zero.internal.application.user.dto.ResetPasswordRequest;
import com.act.modules.zero.internal.application.user.dto.SysUserDTO;
import com.act.modules.zero.internal.application.user.dto.UpdatePersonalInfoRequest;
import com.act.modules.zero.internal.domain.SysUser;
import com.act.modules.zero.internal.mapper.SysUserMapper;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@Api(tags = "系统用户")
@RestController
@RequestMapping("/api/v1/sysUsers/")
@SuppressWarnings("all")
public class SysUserController extends BaseController<SysUser, SysUserDTO, SysUserMapper> {

    @Autowired
    private SysUserService _user;

    @ApiOperation(value = "登录")
    @PostMapping("auth/token")
    public AjaxResponse<Object> login(@RequestBody LoginDTO request) throws InstantiationException, IllegalAccessException, FriendlyException {
        return _user.login(request);
    }

    @ApiOperation(value = "重置密码")
    @PostMapping("resetPassword")
    public AjaxResponse<Object> resetPassword(@RequestBody ResetPasswordRequest request) {
        return _user.resetPassword(request);
    }

    @ApiOperation(value = "修改个人信息")
    @PostMapping("updatePersonalInfo")
    public AjaxResponse<Object> UpdatePersonalInfo(@RequestBody UpdatePersonalInfoRequest request) {
        return _user.updatePersonalInfo(request);
    }
}
