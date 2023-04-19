package com.act.modules.zero;

import com.act.core.application.DynamicFilter;
import com.act.core.application.PageDto;
import com.act.core.utils.JWTUtils;
import com.act.core.utils.StringExtensions;
import com.act.modules.zero.application.services.sysUser.SysUserService;
import com.act.modules.zero.application.services.sysUser.dto.LoginDTO;
import com.act.modules.zero.application.services.sysUser.dto.SysUserDTO;
import com.act.modules.zero.domain.SysUser;
import com.act.modules.zero.mapper.SysUserMapper;
import lombok.var;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import javax.annotation.Resource;
import java.util.ArrayList;

@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = {ZeroApplication.class, ZeroApplicationTests.class})
@MapperScan({"com.act.modules.zero.mapper"})
class ZeroApplicationTests {

    @Test
    void contextLoads() {
    }

    @Resource
    private SysUserMapper _sysUserMapper;

    @Resource
    private SysUserService _sysUserService;

    @Test
    public void GetAll() {
        SysUser users = _sysUserMapper.selectById(1);
        System.out.println(users.getUserName());
    }

    @Test
    public void Delete_Test() {
        _sysUserService.Delete(7L);
    }

    @Test
    public void PageSearch_Test() {

        var page = new PageDto();

        var filter = new DynamicFilter();
        filter.setField("id");
        filter.setOperate("Equal");
        filter.setValue(7);

        var filters = new ArrayList<DynamicFilter>();
        filters.add(filter);

        page.setDynamicFilters(filters);

        page.setPageIndex(1);
        page.setPageSize(10);
        var one = _sysUserService.PageSearch(page);
        System.out.println(one.getDatas());
    }

    @Test
    public void Login_Test() throws InstantiationException, IllegalAccessException {
        var loginModel = new LoginDTO();
        loginModel.setName("admin");
        loginModel.setPassword("qwe213QWE");
        var result = _sysUserService.Login(loginModel);
        System.out.println(result.getData());

        var token = (String) result.getData();

        JWTUtils.parseClaimsJws(token);
    }

    @Test
    public void SysUser_CreateOrEdit() throws InstantiationException, IllegalAccessException {

        var request = new SysUserDTO();
        request.setId(7L);
        request.setUserName("phil");
        request.setRoleId(1L);
        request.setPassword(StringExtensions.ToMd5("123qwe").toUpperCase());
        var one = _sysUserService.CreateOrEdit(request);
        System.out.println(one);
    }
}
