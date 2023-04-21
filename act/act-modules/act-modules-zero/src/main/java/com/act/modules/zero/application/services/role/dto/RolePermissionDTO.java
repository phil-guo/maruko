package com.act.modules.zero.application.services.role.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class RolePermissionDTO {

    private Long menuId;
    private ArrayList<Long> operates;

    public RolePermissionDTO() {
        operates = new ArrayList<>();
    }
}
