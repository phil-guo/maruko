package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.menu.SysMenuService;
import com.act.modules.zero.internal.application.menu.dto.MenusRoleRequest;
import com.act.modules.zero.internal.application.menu.dto.SysMenuDTO;
import com.act.modules.zero.internal.domain.SysMenu;
import com.act.modules.zero.internal.mapper.SysMenuMapper;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@Api(tags = "系统菜单")
@RestController
@RequestMapping("/api/v1/sysMenus/")
@SuppressWarnings("all")
public class SysMenuController extends BaseController<SysMenu, SysMenuDTO, SysMenuMapper> {

    @Autowired
    private SysMenuService _menu;

    @ApiOperation(value = "获取菜单功能")
    @GetMapping("getMenuOfOperate")
    public AjaxResponse<Object> getMenuOfOperate(long id) {
        return _menu.getMenuOfOperate(id);
    }

    @ApiOperation(value = "获取所有父级菜单")
    @GetMapping("getAllParentMenus")
    public AjaxResponse<Object> getAllParentMenus() {
        return _menu.getAllParentMenus();
    }

    @ApiOperation(value = "根据角色获取菜单")
    @PostMapping("getMenusByRole")
    public AjaxResponse<Object> GetMenusByRole(@RequestBody MenusRoleRequest request) {
        return new AjaxResponse<Object>(_menu.getMenusByRole(request));
    }

    @ApiOperation(value = "获取角色设置的菜单")
    @PostMapping("getMenusSetRole")
    public AjaxResponse<Object> GetMenusSetRole(@RequestBody MenusRoleRequest request) {
        return new AjaxResponse<Object>(_menu.getMenusSetRole(request));
    }
}
