package com.act.modules.zero.application.services.menu.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class RoleMenuResponse {
    private List<String> menuIds;
    private List<MenuModel> list;

    public RoleMenuResponse() {
        menuIds = new ArrayList<>();
        list = new ArrayList<>();
    }
}
