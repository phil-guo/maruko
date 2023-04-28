package com.act.modules.zero.internal.application.operate;

import com.act.core.application.CurdAppService;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.internal.application.menu.SysMenuService;
import com.act.modules.zero.internal.application.operate.dto.*;
import com.act.modules.zero.internal.application.role.SysRoleMenuService;
import com.act.modules.zero.internal.domain.SysMenu;
import com.act.modules.zero.internal.domain.SysOperate;
import com.act.modules.zero.internal.domain.SysRoleMenu;
import com.act.modules.zero.internal.mapper.SysOperateMapper;
import com.alibaba.fastjson.JSON;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import lombok.var;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
@SuppressWarnings("all")
public class SysOperateServiceImp extends CurdAppService<SysOperate, SysOperateDTO, SysOperateMapper> implements SysOperateService {

    @Autowired
    private SysRoleMenuService _roleMenu;

    @Autowired
    private SysMenuService _menu;

    @Override
    public SysOperateDTO createOrEdit(SysOperateDTO request) throws InstantiationException, IllegalAccessException {

        if (request == null)
            return null;

        SysOperate data = new SysOperate();

        if (request.getId() == 0) {
            var entity = Table().selectOne(new LambdaQueryWrapper<SysOperate>()
                    .orderBy(true, false, SysOperate::getUnique));
            if (entity != null)
                request.setUnique(entity.getUnique() + 1);
            else
                request.setUnique(10001);
            BeanUtilsExtensions.copyProperties(request, data);
            Table().insert(data);
        } else {
            data = Table().selectOne(new LambdaQueryWrapper<SysOperate>()
                    .eq(SysOperate::getId, request.getId()));
            data.setName(request.getName());
            data.setRemark(request.getRemark());
            data.setIsBasicData(request.getIsBasicData());
            Table().updateById(data);
        }

        var result = request.getClass().newInstance();
        BeanUtilsExtensions.copyProperties(data, result);
        return result;
    }

    public MenuOfOperateResponse getMenuOfOperate(MenuOfOperateRequest request) {

        var idNos = new MenuOfOperateResponse();
        var roleMenu = this._roleMenu.Table()
                .selectOne(new LambdaQueryWrapper<SysRoleMenu>()
                        .eq(SysRoleMenu::getRoleId, request.getRoleId())
                        .eq(SysRoleMenu::getMenuId, request.getMenuId()));

        List<Long> roleMenuOperates;

        if (roleMenu == null)
            roleMenuOperates = new ArrayList<>();
        else {
            roleMenuOperates = JSON.parseArray(roleMenu.getOperates()).toJavaList(Long.class);
        }

        var menu = this._menu.Table().selectOne(new LambdaQueryWrapper<SysMenu>()
                .eq(SysMenu::getId, request.getMenuId()));

        List<Long> menuOperates;
        if (menu == null)
            menuOperates = new ArrayList<>();
        else {
            menuOperates = JSON.parseArray(menu.getOperates()).toJavaList(Long.class);
        }
        //取差集
        List<Long> operates = menuOperates.stream()
                .filter(roleMenuOperates::contains)
                .collect(Collectors.toList());

        operates.forEach(item -> {
            var operate = Table().selectOne(new LambdaQueryWrapper<SysOperate>()
                    .eq(SysOperate::getId, item));
            idNos.getDatas().add(Integer.toString(operate.getUnique()));
        });
        return idNos;
    }

    public GetMenuOfOperateByRoleResponse getMenuOfOperateByRole(GetMenuOfOperateByRoleRequest request) throws FriendlyException {
        var result = new GetMenuOfOperateByRoleResponse();
        var menu = this._menu.Table().selectOne(new LambdaQueryWrapper<SysMenu>()
                .eq(SysMenu::getKey, request.getKey()));
        if (menu == null)
            throw new FriendlyException("key" + request.getKey() + "没有对应的菜单！");

        var roleMenu = this._roleMenu.Table()
                .selectOne(new LambdaQueryWrapper<SysRoleMenu>()
                        .eq(SysRoleMenu::getRoleId, request.getRoleId())
                        .eq(SysRoleMenu::getMenuId, menu.getId()));
        if (roleMenu == null)
            throw new FriendlyException("你还没有配置该页面的功能权限！");

        JSON.parseArray(roleMenu.getOperates()).toJavaList(Long.class).forEach(item -> {
            var operate = Table().selectById(item);
            var data = new GetMenuOfOperateByRoleResponseData();
            data.setUnique(operate.getUnique());
            data.setName(operate.getName());
            result.getDatas().add(data);
        });
        return result;
    }
}
