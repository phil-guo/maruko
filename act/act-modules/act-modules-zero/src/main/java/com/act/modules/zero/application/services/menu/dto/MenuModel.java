package com.act.modules.zero.application.services.menu.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class MenuModel {
    private String id;
    private String label;
    private List<MenuModel> children;

    public MenuModel(){
        children = new ArrayList<>();
    }
}
