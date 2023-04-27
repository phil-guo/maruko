package com.act.modules.zero.application.operate.dto;

import lombok.Data;

@Data
public class GetMenuOfOperateByRoleRequest {
    private long roleId;
    private String key;
}
