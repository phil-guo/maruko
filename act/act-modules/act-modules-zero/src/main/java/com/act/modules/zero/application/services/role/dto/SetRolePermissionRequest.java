package com.act.modules.zero.application.services.role.dto;

import lombok.Data;

import java.util.ArrayList;

@Data
public class SetRolePermissionRequest {
    private Integer roleId;
    private ArrayList<String> menuIds;

    public SetRolePermissionRequest() {
        menuIds = new ArrayList<>();
    }
}
