package com.act.modules.zero.application.services.operate.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class GetMenuOfOperateByRoleResponse {
    private List<GetMenuOfOperateByRoleResponseData> datas;

    public GetMenuOfOperateByRoleResponse() {
        datas = new ArrayList<>();
    }
}
