package com.act.modules.starmap.config;

public class StarMapConfig {
    public static String prefix = "starMap";

    public static String getKey(String key) {
        return prefix + ":" + key;
    }
}
