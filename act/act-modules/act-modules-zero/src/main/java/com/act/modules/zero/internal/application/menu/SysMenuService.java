package com.act.modules.zero.internal.application.menu;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.internal.application.menu.dto.MenusRoleResponse;
import com.act.modules.zero.internal.application.menu.dto.MenusRoleRequest;
import com.act.modules.zero.internal.application.menu.dto.RoleMenuResponse;
import com.act.modules.zero.internal.application.menu.dto.SysMenuDTO;
import com.act.modules.zero.internal.domain.SysMenu;
import com.act.modules.zero.internal.mapper.SysMenuMapper;

import java.util.List;

@SuppressWarnings("all")
public interface SysMenuService extends ICurdAppService<SysMenu, SysMenuDTO, SysMenuMapper> {

    /**
     * 获取设置角色的菜单数据
     *
     * @param request
     * @return
     */
    RoleMenuResponse getMenusSetRole(MenusRoleRequest request);

    /**
     * 根据角色获取菜单
     *
     * @param request
     * @return
     */
    List<MenusRoleResponse> getMenusByRole(MenusRoleRequest request);

    /**
     * 获取所有的父级菜单
     *
     * @return
     */
    AjaxResponse<Object> getAllParentMenus();

    /**
     * 获取菜单操作
     *
     * @param id 菜单主键
     * @return
     * @throws FriendlyException
     */
    AjaxResponse<Object> getMenuOfOperate(long id) throws FriendlyException;
}
