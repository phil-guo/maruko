package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.operate.SysOperateService;
import com.act.modules.zero.internal.application.operate.dto.GetMenuOfOperateByRoleRequest;
import com.act.modules.zero.internal.application.operate.dto.MenuOfOperateRequest;
import com.act.modules.zero.internal.application.operate.dto.SysOperateDTO;
import com.act.modules.zero.internal.domain.SysOperate;
import com.act.modules.zero.internal.mapper.SysOperateMapper;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Api(tags = "系统功能")
@RestController
@RequestMapping("/api/v1/sysOperate/")
@SuppressWarnings("all")
public class SysOperateController extends BaseController<SysOperate, SysOperateDTO, SysOperateMapper> {

    @Autowired
    private SysOperateService _operate;

    @ApiOperation(value = "获取菜单的功能")
    @PostMapping("getMenuOfOperate")
    public AjaxResponse<Object> getMenuOfOperate(@RequestBody MenuOfOperateRequest request) {
        return new AjaxResponse<Object>(_operate.getMenuOfOperate(request));
    }

    @ApiOperation(value = "获取角色下菜单的功能")
    @PostMapping("getMenuOfOperateByRole")
    public AjaxResponse<Object> GetMenuOfOperateByRole(@RequestBody GetMenuOfOperateByRoleRequest request) {
        return new AjaxResponse<Object>(_operate.getMenuOfOperateByRole(request));
    }
}
