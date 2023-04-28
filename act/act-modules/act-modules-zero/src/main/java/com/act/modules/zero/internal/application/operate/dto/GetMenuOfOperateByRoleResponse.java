package com.act.modules.zero.internal.application.operate.dto;

import lombok.Data;

import java.util.ArrayList;
import java.util.List;

@Data
public class GetMenuOfOperateByRoleResponse {
    private List<GetMenuOfOperateByRoleResponseData> datas = new ArrayList<>();
}
