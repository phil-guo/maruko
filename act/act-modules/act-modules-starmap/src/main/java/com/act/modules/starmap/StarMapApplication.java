package com.act.modules.starmap;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan(basePackages = {"com.act.modules.starmap.*", "com.act.core.*"})
public class StarMapApplication {

    public static void main(String[] args) {
        SpringApplication.run(StarMapApplication.class, args);
    }

}
