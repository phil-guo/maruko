package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.role.SysRoleService;
import com.act.modules.zero.internal.application.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.internal.application.role.dto.SysRoleDTO;
import com.act.modules.zero.internal.domain.SysRole;
import com.act.modules.zero.internal.mapper.SysRoleMapper;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@Api(tags = "系统角色")
@RestController
@RequestMapping("/api/v1/sysRole/")
@SuppressWarnings("all")
public class SysRoleController extends BaseController<SysRole, SysRoleDTO, SysRoleMapper> {

    @Autowired
    private SysRoleService _role;

    @ApiOperation(value = "获取所有角色")
    @GetMapping("getAllRoles")
    public AjaxResponse<Object> GetAllRoles() {
        return _role.getAllRoles();
    }

    @ApiOperation(value = "设置角色权限")
    @PostMapping("setRolePermission")
    public AjaxResponse<Object> SetRolePermission(@RequestBody SetRolePermissionRequest request) {
        return new AjaxResponse<Object>(_role.setRolePermission(request));
    }


}
