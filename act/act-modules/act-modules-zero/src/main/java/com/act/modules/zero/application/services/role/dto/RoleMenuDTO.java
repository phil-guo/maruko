package com.act.modules.zero.application.services.role.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class RoleMenuDTO {
    private long id;
    private long parentId;
    private String title;
    private String icon;
    private String path;
    private String key;
    private List<RoleMenuDTO> children;
    private String operates;

    public RoleMenuDTO() {
        children = new ArrayList<>();
    }
}
