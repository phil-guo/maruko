package com.act.modules.zero.internal.application.menu.dto;

import com.act.core.domain.BaseEntity;
import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
@SuppressWarnings("all")
public class SysMenuDTO extends BaseEntity<Long> {

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
