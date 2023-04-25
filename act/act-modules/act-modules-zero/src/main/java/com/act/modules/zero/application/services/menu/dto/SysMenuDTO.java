package com.act.modules.zero.application.services.menu.dto;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Data
public class SysMenuDTO extends BaseEntity<Long> {
    /*    private Long id;
        private LocalDateTime createTime;*/
    private Long parentId;

    private String name;

    private String url;

    private int level;

    private String operates;

    private int sort;

    private String icon;

    private String key;

    private int isLeftShow;

    private List<SysMenuDTO> children = new ArrayList<>();
}
