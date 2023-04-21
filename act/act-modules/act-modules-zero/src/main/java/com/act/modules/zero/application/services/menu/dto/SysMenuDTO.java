package com.act.modules.zero.application.services.menu.dto;

import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
public class SysMenuDTO {
    private Long id;
    private LocalDateTime createTime;
    private Long parentId;

    private String name;

    private String url;

    private int level;

    private String operates;

    private int sort;

    private String icon;

    private String key;

    private int isLeftShow;
}
