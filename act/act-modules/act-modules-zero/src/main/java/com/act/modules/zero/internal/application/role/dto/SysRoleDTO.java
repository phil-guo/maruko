package com.act.modules.zero.internal.application.role.dto;

import com.act.core.domain.BaseEntity;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@SuppressWarnings("all")
public class SysRoleDTO extends BaseEntity<Long> {

    /*名称*/
    private String name;

    /*备注*/
    private String remark;
}
