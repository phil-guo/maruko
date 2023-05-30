package com.act.modules.starmap;

import com.act.core.redis.RedisUtils;
import com.act.modules.starmap.config.StarMapConfig;
import lombok.var;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = {StarMapApplication.class, StarMapApplicationTests.class})
@ComponentScan(basePackages = {"com.act.modules.starmap.*", "com.act.core.*"})
@MapperScan({"com.act.modules.starmap.mapper"})
@SuppressWarnings("all")
class StarMapApplicationTests {

    @Autowired
    private RedisUtils _redis;

    @Autowired
    private StarMapConfig _config;


    @Test
    void contextLoads() {
    }

    @Test
    public void AddValueToRedis() {
        var a = _config.getPrefix("test1");
        var one = _redis.set(a, "123");
        var two = _redis.set(_config.getPrefix("test2"), "321");
        System.out.println(one);
        System.out.println(two);
    }
}
