package com.act.modules.zero.internal.application.menu.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class MenuModel {
    private String id;
    private String label;
    private List<MenuModel> children = new ArrayList<>();
}
