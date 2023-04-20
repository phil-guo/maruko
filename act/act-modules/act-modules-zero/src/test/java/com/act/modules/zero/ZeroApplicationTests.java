package com.act.modules.zero;

import com.act.core.application.DynamicFilter;
import com.act.core.application.PageDto;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.core.utils.JWTUtils;
import com.act.core.utils.StringExtensions;
import com.act.modules.zero.application.services.sysUser.SysUserService;
import com.act.modules.zero.application.services.sysUser.dto.LoginDTO;
import com.act.modules.zero.application.services.sysUser.dto.ResetPasswordRequest;
import com.act.modules.zero.application.services.sysUser.dto.SysUserDTO;
import lombok.var;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import javax.annotation.Resource;
import java.util.ArrayList;

@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = {ZeroApplication.class, ZeroApplicationTests.class,})
//@MapperScan({"com.act.modules.zero.mapper"})
class ZeroApplicationTests {

    @Test
    void contextLoads() {
    }

    @Resource
    private SysUserService userService;

    @Test
    public void resetPassword_Test() throws FriendlyException {
        var request = new ResetPasswordRequest();
        request.setUserId(7);
        var one = userService.resetPassword(request);
        System.out.println(one);
    }

    @Test
    public void Delete_Test() throws FriendlyException {
        userService.delete(7L);
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

        page.setPageIndex(1);
        page.setPageSize(10);
        var one = userService.pageSearch(page);
        System.out.println(one.getDatas());
    }

    @Test
    public void Login_Test() throws InstantiationException, IllegalAccessException, FriendlyException {
        var loginModel = new LoginDTO();
        loginModel.setName("admin");
        loginModel.setPassword("qwe213QWE");
        AjaxResponse<Object> result = userService.login(loginModel);
        System.out.println(result.getData());

        var token = (String) result.getData();

        JWTUtils.parseClaimsJws(token);
    }

    @Test
    public void SysUser_CreateOrEdit() throws InstantiationException, IllegalAccessException, FriendlyException {

        var request = new SysUserDTO();
        request.setId(7L);
        request.setUserName("phil");
        request.setRoleId(1L);
        request.setPassword(StringExtensions.ToMd5("123qwe").toUpperCase());
        var one = userService.createOrEdit(request);
        System.out.println(one);
    }
}
