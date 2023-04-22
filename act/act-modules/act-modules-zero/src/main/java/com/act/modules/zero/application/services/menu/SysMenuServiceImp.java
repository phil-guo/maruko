package com.act.modules.zero.application.services.menu;

import com.act.core.application.ComBoxInfo;
import com.act.core.application.CurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.application.services.menu.dto.MenusRoleRequest;
import com.act.modules.zero.application.services.menu.dto.MenusRoleResponse;
import com.act.modules.zero.application.services.menu.dto.SysMenuDTO;
import com.act.modules.zero.application.services.operate.SysOperateService;
import com.act.modules.zero.application.services.page.PageService;
import com.act.modules.zero.application.services.role.SysRoleMenuService;
import com.act.modules.zero.application.services.role.dto.RoleMenuDTO;
import com.act.modules.zero.domain.Page;
import com.act.modules.zero.domain.SysMenu;
import com.act.modules.zero.domain.SysOperate;
import com.act.modules.zero.domain.SysRoleMenu;
import com.act.modules.zero.mapper.SysMenuMapper;
import com.alibaba.fastjson.JSON;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;
import org.aspectj.weaver.loadtime.Aj;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class SysMenuServiceImp extends CurdAppService<SysMenu, SysMenuDTO, SysMenuMapper> implements SysMenuService {

    @Autowired
    private PageService _page;
    @Autowired
    private SysOperateService _operate;
    @Autowired
    private SysRoleMenuService _roleMenu;

    public List<MenusRoleResponse> getMenusByRole(MenusRoleRequest request) {
        var result = new ArrayList<MenusRoleResponse>();
        var tree = GetRoleOfMenus(request.getRoleId(), true);
        var home = new MenusRoleResponse();
        home.setId(0L);
        home.setIcon("el-icon-platform-eleme");
        home.setPath("/home");
        home.setTitle("首页");

        tree.forEach(item -> {
            var model = new MenusRoleResponse();
            BeanUtilsExtensions.copyProperties(item, model);
            if (item.getChildren().size() > 0)
                item.getChildren().forEach(child -> {
                    var childModel = new MenusRoleResponse();
                    BeanUtilsExtensions.copyProperties(child, childModel);
                    model.getChildren().add(childModel);
                });
            result.add(model);
        });

        return result;
    }

    public AjaxResponse<Object> getAllParentMenus() {

        var data = Table().selectList(new MPJLambdaWrapper<SysMenu>()
                .select(SysMenu::getName, SysMenu::getId)
                .eq(SysMenu::getParentId, 99999)
                .eq(SysMenu::getIsLeftShow, true)
        );

        var comBoxList = new ArrayList<ComBoxInfo>();

        data.forEach(item -> {
            var comBox = new ComBoxInfo();
            comBox.setKey(item.getName());
            comBox.setValue(item.getId());
            comBoxList.add(comBox);
        });

        return new AjaxResponse<>(comBoxList);
    }

    public AjaxResponse<Object> getMenuOfOperate(long id) throws FriendlyException {
        var menu = getById(id);
        if (menu == null)
            throw new FriendlyException("菜单不存在！");

        if (StringUtils.isEmpty(menu.getOperates()))
            return new AjaxResponse<Object>(new ArrayList<>());

        var operateIds = JSON.parseArray(menu.getOperates()).toJavaList(long.class);
        var result = _operate.Table()
                .selectList(new MPJLambdaWrapper<SysOperate>()
                        .select(SysOperate::getName, SysOperate::getUnique)
                        .in(SysOperate::getId, operateIds));
        return new AjaxResponse<>(result);
    }

    @Override
    public SysMenuDTO createOrEdit(SysMenuDTO request) throws FriendlyException {

        if (request == null)
            return null;

        var data = new SysMenu();
        if (request.getId() > 0) {
            data = Table().selectById(request.getId());
            if (!data.getName().equals(request.getName()) || !data.getKey().equals(request.getKey())) {
                var oldPage = _page.Table().selectOne(new LambdaQueryWrapper<Page>()
                        .eq(Page::getKey, data.getKey()));
                var newPage = new Page();
                newPage.setId(oldPage.getId());
                newPage.setKey(request.getKey());
                newPage.setName(request.getName());
                _page.updateById(newPage);
            } else {
                data.setOperates(request.getOperates());
                data.setParentId(request.getParentId());
                data.setName(request.getName());
                data.setLevel(request.getLevel());
                data.setUrl(request.getUrl());
                data.setIcon(request.getIcon());
                data.setIsLeftShow(request.getIsLeftShow());
                updateById(data);
            }
        } else {
            var lastMenu = Table().selectOne(new LambdaQueryWrapper<SysMenu>()
                    .orderBy(true, false, SysMenu::getId));
            if (lastMenu != null && request.getId() == 0)
                request.setSort(lastMenu.AddOperateSort());

            var key = StringUtils.isEmpty(request.getKey()) ? "" : request.getKey();

            if (_page.Table().exists(new LambdaQueryWrapper<Page>().eq(Page::getKey, key)))
                throw new FriendlyException("已经存在Key：【" + key + "】");

            BeanUtilsExtensions.copyProperties(request, data);
            save(data);
            var page = new Page();
            page.setName(request.getName());
            page.setKey(request.getKey());
            _page.save(page);
        }
        var result = new SysMenuDTO();
        BeanUtilsExtensions.copyProperties(data, result);
        return result;
    }

    private List<RoleMenuDTO> GetRoleOfMenus(long roleId, Boolean isLeftShow) {
        var datas = GetRoleMenu(roleId, isLeftShow);
        var listMenus = new ArrayList<RoleMenuDTO>();
        datas.forEach(item -> {
            var dto = new RoleMenuDTO();
            dto.setParentId(item.getParentId());
            dto.setId(item.getId());
            dto.setTitle(item.getName());
            dto.setIcon(item.getIcon());
            dto.setPath(item.getUrl());
            dto.setOperates(item.getOperates());
            dto.setKey(item.getKey());
            listMenus.add(dto);
        });

        var tree = listMenus.stream().filter(item -> item.getParentId() == 99999).collect(Collectors.toList());
        tree.forEach(item -> BuildRoleMenusRecursiveTree(listMenus, item));
        return tree;
    }

    private void BuildRoleMenusRecursiveTree(List<RoleMenuDTO> list, RoleMenuDTO currentTree) {
        list.forEach(item ->
        {
            if (item.getParentId() == currentTree.getId())
                currentTree.getChildren().add(item);
        });
    }

    private List<SysMenu> GetRoleMenu(long roleId, Boolean isLeftShow) {
        var menus = new ArrayList<SysMenu>();
        var roleMenusByRole = _roleMenu.Table().selectList(new MPJLambdaWrapper<SysRoleMenu>()
                .eq(SysRoleMenu::getRoleId, roleId)
                .orderByAsc(SysRoleMenu::getMenuId)
        );
        if (roleMenusByRole.size() == 0)
            return menus;

        roleMenusByRole.forEach(item -> {
            SysMenu menu;
            if (isLeftShow != null) {
                menu = getOne(new LambdaQueryWrapper<SysMenu>()
                        .eq(SysMenu::getId, item.getMenuId())
                        .eq(SysMenu::getIsLeftShow, isLeftShow));
            } else {
                menu = getById(item.getMenuId());
            }

            if (menu == null)
                return;
            menu.setOperates(item.getOperates());
            menus.add(menu);
        });
        return menus;
    }
}
