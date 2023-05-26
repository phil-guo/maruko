package com.act.modules.starmap;

import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

@RunWith(SpringJUnit4ClassRunner.class)
@SpringBootTest(classes = {StarMapApplication.class, StarMapApplicationTests.class,})
@MapperScan({"com.act.modules.zero.mapper"})
@SuppressWarnings("all")
class StarMapApplicationTests {

    @Test
    void contextLoads() {
    }

}
