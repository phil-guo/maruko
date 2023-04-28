package com.act.modules.zero.internal.application.role.dto;

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
    private List<RoleMenuDTO> children= new ArrayList<>();
    private String operates;
}
