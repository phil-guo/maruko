package com.act.modules.starmap.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Service;

@Service
@SuppressWarnings("all")
public class StarMapConfig {

    @Autowired
    private Environment _environment;

    /*
     * 获取前缀
     */
    public String getPrefix(String key) {
        return _environment.getProperty("starMap.prefix") + ":" + key;
    }
}
