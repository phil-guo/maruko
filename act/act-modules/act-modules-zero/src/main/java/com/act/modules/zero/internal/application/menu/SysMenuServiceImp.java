package com.act.modules.zero.internal.application.menu;

import com.act.core.application.ComBoxInfo;
import com.act.core.application.CurdAppService;
import com.act.core.application.PageDto;
import com.act.core.application.PagedResultDto;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.core.utils.WrapperExtensions;
import com.act.modules.zero.internal.application.menu.dto.*;
import com.act.modules.zero.internal.application.operate.SysOperateService;
import com.act.modules.zero.internal.application.page.PageService;
import com.act.modules.zero.internal.application.role.SysRoleMenuService;
import com.act.modules.zero.internal.application.role.SysRoleService;
import com.act.modules.zero.internal.application.role.dto.RoleMenuDTO;
import com.act.modules.zero.internal.domain.*;
import com.act.modules.zero.internal.mapper.SysMenuMapper;
import com.alibaba.fastjson.JSON;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
@SuppressWarnings("all")
public class SysMenuServiceImp extends CurdAppService<SysMenu, SysMenuDTO, SysMenuMapper> implements SysMenuService {

    @Autowired
    private PageService _page;
    @Autowired
    private SysOperateService _operate;
    @Autowired
    private SysRoleMenuService _roleMenu;

    @Autowired
    private SysRoleService _role;

    public RoleMenuResponse getMenusSetRole(MenusRoleRequest request) {
        var result = new RoleMenuResponse();

        var data = Table().selectList(new LambdaQueryWrapper<SysMenu>()
                .orderByAsc(SysMenu::getId));

        var listMenus = new ArrayList<RoleMenuDTO>();

        data.forEach(item -> {
            var menuModel = new RoleMenuDTO();
            menuModel.setParentId(item.getParentId());
            menuModel.setId(item.getId());
            menuModel.setTitle(item.getName());
            menuModel.setIcon(item.getIcon());
            menuModel.setPath(item.getUrl());
            menuModel.setOperates(item.getOperates());
            listMenus.add(menuModel);
        });

        var tree = listMenus.stream().filter(item -> item.getParentId() == 99999).collect(Collectors.toList());

        var operates = _operate.Table().selectList(new LambdaQueryWrapper<SysOperate>()
                .select(SysOperate::getId, SysOperate::getUnique, SysOperate::getName));

        tree.forEach(item -> BuildRoleMenusRecursiveTree(listMenus, item));
        tree.forEach(item -> {
            var model = new MenuModel();
            model.setId(item.getId() + "_0");
            model.setLabel(item.getTitle());

            if (item.getChildren().size() > 0) {
                item.getChildren().forEach(child -> {
                    var operateModel = new MenuModel();
                    operateModel.setId(child.getId() + "_0");
                    operateModel.setLabel(child.getTitle());
                    model.getChildren().add(operateModel);
                    operates.forEach(op -> {
                        var opIds = JSON.parseArray(child.getOperates()).toJavaList(Long.class);
                        if (opIds.contains(op.getId())) {
                            var opModel = new MenuModel();
                            opModel.setId(child.getId() + "_" + op.getId());
                            opModel.setLabel(op.getName());
                            operateModel.getChildren().add(opModel);
                        }
                    });
                });
            } else {
                operates.forEach(op -> {
                    var opIds = JSON.parseArray(item.getOperates()).toJavaList(Long.class);
                    if (opIds.contains(op.getId())) {
                        var opModel = new MenuModel();
                        opModel.setId(item.getId() + "_" + op.getId());
                        opModel.setLabel(op.getName());
                        model.getChildren().add(opModel);
                    }
                });
            }

            result.getList().add(model);
        });

        var roleMenus = GetRoleOfMenus(request.getRoleId(), null);
        roleMenus.forEach(item -> {
            result.getMenuIds().add(item.getId() + "_0");
            if (item.getChildren().size() > 0) {
                item.getChildren().forEach(child -> {
                    result.getMenuIds().add(child.getId() + "_0");
                    var opIds = JSON.parseArray(child.getOperates()).toJavaList(Long.class);
                    opIds.forEach(operateId -> {
                        operates.forEach(op -> {
                            if (!op.getId().equals(operateId))
                                return;
                            result.getMenuIds().add(child.getId() + "_" + op.getId());
                        });
                    });
                });
            } else {
                var opIds = JSON.parseArray(item.getOperates()).toJavaList(Long.class);
                opIds.forEach(operateId -> {
                    operates.forEach(op -> {
                        if (!op.getId().equals(operateId))
                            return;
                        result.getMenuIds().add(item.getId() + "_" + op.getId());
                    });
                });
            }
        });

        return result;
    }

    public List<MenusRoleResponse> getMenusByRole(MenusRoleRequest request) {
        var result = new ArrayList<MenusRoleResponse>();
        var tree = GetRoleOfMenus(request.getRoleId(), true);
        var home = new MenusRoleResponse();
        home.setId(0L);
        home.setIcon("el-icon-platform-eleme");
        home.setPath("/home");
        home.setTitle("首页");
        result.add(home);
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

    @Override
    public void delete(Long id) throws FriendlyException {
        //删除验证
        var roleMenus = _roleMenu.Table()
                .selectList(new LambdaQueryWrapper<SysRoleMenu>()
                        .eq(SysRoleMenu::getMenuId, id));

        if (roleMenus.size() > 0) {
            var roleIds = roleMenus.stream().map(SysRoleMenu::getRoleId).collect(Collectors.toList());
            var roles = _role.Table()
                    .selectList(new LambdaQueryWrapper<SysRole>()
                            .in(SysRole::getId, roleIds));
            var roleNames = roles.stream().map(SysRole::getName).collect(Collectors.toList());
            throw new FriendlyException("请先解除角色[" + String.join(",", roleNames) + "]权限中的菜单关系，在删除菜单");
        }
        super.delete(id);
    }

    @Override
    public PagedResultDto pageSearch(PageDto search) {
        List<SysMenu> topNode = new ArrayList<SysMenu>();
        if (search.getDynamicFilters().size() > 0) {
            var queryWrapper = WrapperExtensions.<SysMenu>ConvertToWrapper(search.getDynamicFilters());
            topNode = Table().selectList(queryWrapper);
        } else {
            topNode = Table().selectList(new LambdaQueryWrapper<SysMenu>()
                    .eq(SysMenu::getParentId, 99999)
                    .orderByDesc(SysMenu::getCreateTime)
            );
        }

        var nodes = Table().selectList(new LambdaQueryWrapper<SysMenu>()
                .orderByDesc(SysMenu::getCreateTime));

        var topNodes = BeanUtilsExtensions.copyListProperties(topNode, SysMenuDTO::new);
        var nodesDTO = BeanUtilsExtensions.copyListProperties(nodes, SysMenuDTO::new);

        topNodes.forEach(item -> BuildMenusRecursiveTree(nodesDTO, item));

        return new PagedResultDto(topNodes.size(), topNodes);
    }

    private void BuildMenusRecursiveTree(List<SysMenuDTO> menus, SysMenuDTO topNode) {
        menus.forEach(item -> {
            if (item.getParentId().equals(topNode.getId()))
                topNode.getChildren().add(item);
        });
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
