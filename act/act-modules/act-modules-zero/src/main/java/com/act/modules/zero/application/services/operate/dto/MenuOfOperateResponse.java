package com.act.modules.zero.application.services.operate.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;
@Data
public class MenuOfOperateResponse {
    private List<String> datas;

    public MenuOfOperateResponse() {
        datas = new ArrayList<>();
    }
}
