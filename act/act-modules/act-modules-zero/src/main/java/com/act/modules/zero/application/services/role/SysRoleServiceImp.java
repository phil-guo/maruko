package com.act.modules.zero.application.services.role;

import com.act.core.application.CurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.application.services.menu.SysMenuService;
import com.act.modules.zero.application.services.role.dto.RolePermissionDTO;
import com.act.modules.zero.application.services.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.application.services.role.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysMenu;
import com.act.modules.zero.domain.SysRole;
import com.act.modules.zero.domain.SysRoleMenu;
import com.act.modules.zero.mapper.SysRoleMapper;
import com.act.modules.zero.mapper.SysRoleMenuMapper;
import com.alibaba.fastjson.JSON;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.List;

@Service
public class SysRoleServiceImp extends CurdAppService<SysRole, SysRoleDTO, SysRoleMapper> implements SysRoleService {

    @Autowired
    private SysRoleMenuService _roleMenu;

    @Autowired
    private SysMenuService _menu;

    public AjaxResponse<Object> getAllRoles() {

        var data = Table().selectMaps(new MPJLambdaWrapper<SysRole>()
                .selectAs(SysRole::getName, "`key`")
                .selectAs(SysRole::getId, "`value`")
        );
        return new AjaxResponse<>(data);
    }

    public Boolean setRolePermission(SetRolePermissionRequest request) {

        if (request.getMenuIds() == null)
            return false;
        if (request.getMenuIds().size() == 0)
            return true;

        var roleMenus = _roleMenu
                .Table()
                .selectList(new LambdaQueryWrapper<SysRoleMenu>()
                        .eq(SysRoleMenu::getRoleId, request.getRoleId()));
        if (roleMenus == null)
            return false;

        if (roleMenus.size() > 0)
            _roleMenu
                    .Table()
                    .delete(new LambdaQueryWrapper<SysRoleMenu>()
                            .eq(SysRoleMenu::getRoleId, request.getRoleId()));

        var models = new ArrayList<RolePermissionDTO>();
        var roleMenuList = new ArrayList<SysRoleMenu>();

        request.getMenuIds().forEach(item -> {
            var model = new RolePermissionDTO();
            var operateArray = item.split("_");
            var operateId = Long.parseLong(operateArray[1]);
            var menuId = Long.parseLong(operateArray[0]);
            if (operateId == 0) {
                if (models.stream().anyMatch(rp -> rp.getMenuId().equals(menuId)))
                    return;
                model.setMenuId(menuId);
                models.add(model);
            } else {
                var rolePermission = models.stream().filter(rp -> rp.getMenuId() == menuId).findAny();
                if (rolePermission.isPresent()) {
                    var data = rolePermission.get();
                    data.getOperates().add(operateId);
                } else {
                    model.setMenuId(menuId);
                    model.getOperates().add(operateId);
                    models.add(model);
                }
            }
        });

        models.forEach(item -> {
            var menu = _menu.Table().selectOne(new LambdaQueryWrapper<SysMenu>().eq(SysMenu::getId, item.getMenuId()));
            if (menu == null)
                return;
            var roleMenu = new SysRoleMenu();
            roleMenu.setMenuId(item.getMenuId());
            roleMenu.setRoleId(request.getRoleId());
            roleMenu.setOperates(JSON.toJSONString(menu.getParentId() == 0
                    ? new ArrayList<Long>()
                    : item.getOperates()));

            roleMenuList.add(roleMenu);
        });

        return _roleMenu.saveBatch(roleMenuList);
    }

    @Override
    public void delete(Long id) throws FriendlyException {
        if (id == 1)
            throw new FriendlyException("超级管理员不允许被删除！");
        super.delete(id);
    }
}
