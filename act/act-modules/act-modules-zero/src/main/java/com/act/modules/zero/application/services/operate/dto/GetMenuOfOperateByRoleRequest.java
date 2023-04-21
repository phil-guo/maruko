package com.act.modules.zero.application.services.operate.dto;

import lombok.Data;

@Data
public class GetMenuOfOperateByRoleRequest {
    private long roleId;
    private String key;
}
