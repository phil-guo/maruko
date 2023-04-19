package com.act.modules.zero;

import com.act.core.utils.JWTUtils;
import com.act.modules.zero.domain.SysUser;
import com.act.modules.zero.mapper.SysUserMapper;
import com.act.modules.zero.services.sysuser.SysUserService;
import com.act.modules.zero.services.sysuser.dto.LoginDTO;
import lombok.var;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import javax.annotation.Resource;

//@RunWith(SpringRunner.class)
//@SpringBootTest
@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = {ZeroApplication.class, ZeroApplicationTests.class})
@MapperScan({"com.act.modules.zero.mapper"})
//@MapperScan("com.act.modules.zero.services.sysuser")
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
    public void Login_Test() {
        var loginModel = new LoginDTO();
        loginModel.setName("admin");
        loginModel.setPassword("qwe213QWE");
        var result = _sysUserService.Login(loginModel);
        System.out.println(result.getData());

        var token = (String) result.getData();

        JWTUtils.parseClaimsJws(token);
    }
}
