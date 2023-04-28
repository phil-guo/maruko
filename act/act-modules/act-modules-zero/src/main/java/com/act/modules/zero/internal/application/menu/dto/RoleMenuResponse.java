package com.act.modules.zero.internal.application.menu.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class RoleMenuResponse {
    private List<String> menuIds = new ArrayList<>();
    private List<MenuModel> list = new ArrayList<>();
}
