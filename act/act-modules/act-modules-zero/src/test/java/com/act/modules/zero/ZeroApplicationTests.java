package com.act.modules.zero;

import com.act.core.application.DynamicFilter;
import com.act.core.application.PageDto;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.core.utils.JWTUtils;
import com.act.core.utils.StringExtensions;
import com.act.modules.zero.internal.application.dictionary.SysDictionaryService;
import com.act.modules.zero.internal.application.menu.SysMenuService;
import com.act.modules.zero.internal.application.menu.dto.MenusRoleRequest;
import com.act.modules.zero.internal.application.operate.SysOperateService;
import com.act.modules.zero.internal.application.operate.dto.GetMenuOfOperateByRoleRequest;
import com.act.modules.zero.internal.application.operate.dto.MenuOfOperateRequest;
import com.act.modules.zero.internal.application.role.SysRoleService;
import com.act.modules.zero.internal.application.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.internal.application.user.SysUserService;
import com.act.modules.zero.internal.application.user.dto.LoginDTO;
import com.act.modules.zero.internal.application.user.dto.ResetPasswordRequest;
import com.act.modules.zero.internal.application.user.dto.SysUserDTO;
import com.alibaba.fastjson.JSON;
import lombok.var;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import javax.annotation.Resource;
import java.util.ArrayList;

@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = {ZeroApplication.class, ZeroApplicationTests.class,})
@MapperScan({"com.act.modules.zero.mapper"})
@SuppressWarnings("all")
class ZeroApplicationTests {

    @Test
    void contextLoads() {
    }

    @Resource
    private SysUserService userService;

    @Resource
    private SysRoleService roleService;

    @Resource
    private SysOperateService operate;

    @Resource
    private SysMenuService menu;

    @Resource
    private SysDictionaryService datationary;

    @Test
    public void getDictionaryByGroup_Test() {
        var one = datationary.getDictionaryByGroup("QuartzStatus");
        System.out.println(JSON.toJSONString(one));
    }

    @Test
    public void menu_pageSearch_Test() {
        var page = new PageDto();
        var one = menu.pageSearch(page);
        System.out.println(one.getDatas());
    }

    @Test
    public void getMenusSetRole_Test() {
        var request = new MenusRoleRequest();
        request.setRoleId(1L);
        var one = menu.getMenusSetRole(request);
        System.out.println(JSON.toJSONString(one));
    }

    @Test
    public void getMenusByRole_Test() {
        var request = new MenusRoleRequest();
        request.setRoleId(1L);
        var one = menu.getMenusByRole(request);
        System.out.println(JSON.toJSONString(one));
    }

    @Test
    public void getAllParentMenus_Test() {
        var one = menu.getAllParentMenus();
        System.out.println(JSON.toJSONString(one));
    }

    @Test
    public void Menu_getMenuOfOperate_Test() throws FriendlyException {
        var one = menu.getMenuOfOperate(52L);
        System.out.println(JSON.toJSONString(one));

    }

    @Test
    public void getMenuOfOperateByRole_Test() throws FriendlyException {
        var request = new GetMenuOfOperateByRoleRequest();
        request.setKey("oils");
        request.setRoleId(1L);
        var one = operate.getMenuOfOperateByRole(request);
        System.out.println(one);
    }

    @Test
    public void getMenuOfOperate_Test() {
        var request = new MenuOfOperateRequest();
        request.setMenuId(11L);
        request.setRoleId(1L);
        var one = operate.getMenuOfOperate(request);
        System.out.println(one);
    }

    @Test
    public void setRolePermission_Test() {
        var request = new SetRolePermissionRequest();
        var menuIds = new ArrayList<String>();
        menuIds.add("52_1");
        menuIds.add("53_1");
        menuIds.add("53_2");
        menuIds.add("53_3");
        menuIds.add("53_4");
        request.setRoleId(Long.valueOf(1L));
        request.setMenuIds(menuIds);
        roleService.setRolePermission(request);
    }

    @Test
    public void getAllRoles_Test() {
        var one = roleService.getAllRoles();
        System.out.println(JSON.toJSONString(one));
    }

    @Test
    public void resetPassword_Test() throws FriendlyException {
        var request = new ResetPasswordRequest();
        request.setUserId(Integer.valueOf(7));
        var one = userService.resetPassword(request);
        System.out.println(one);
    }

    @Test
    public void Delete_Test() throws FriendlyException {
        userService.delete(Long.valueOf(7L));
    }

    @Test
    public void PageSearch_Test() {

        var page = new PageDto();

        var filter = new DynamicFilter();
        filter.setField("userName");
        filter.setOperate("Equal");
        filter.setValue("phil");

        var filters = new ArrayList<DynamicFilter>();
        filters.add(filter);

        page.setDynamicFilters(filters);

        page.setPageIndex(Integer.valueOf(1));
        page.setPageSize(Integer.valueOf(10));
        var one = userService.pageSearch(page);
        System.out.println(one.getDatas());
    }

    @Test
    public void Login_Test() throws InstantiationException, IllegalAccessException, FriendlyException {
        var loginModel = new LoginDTO();
        loginModel.setName("admin");
        loginModel.setPassword("123qwe");
        AjaxResponse<Object> result = userService.login(loginModel);
        System.out.println(result.getData());
        var token = (String) result.getData();
        JWTUtils.parseClaimsJws(token);
    }

    @Test
    public void SysUser_CreateOrEdit() throws InstantiationException, IllegalAccessException, FriendlyException {

        var request = new SysUserDTO();
        request.setId(Long.valueOf(7L));
        request.setUserName("phil");
        request.setRoleId(Long.valueOf(1L));
        request.setPassword(StringExtensions.ToMd5("123qwe").toUpperCase());
        var one = userService.createOrEdit(request);
        System.out.println(one);
    }
}
