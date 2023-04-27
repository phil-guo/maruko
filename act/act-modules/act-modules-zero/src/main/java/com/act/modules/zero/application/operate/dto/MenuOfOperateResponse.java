package com.act.modules.zero.application.operate.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class MenuOfOperateResponse {
    private List<String> datas = new ArrayList<>();
}
