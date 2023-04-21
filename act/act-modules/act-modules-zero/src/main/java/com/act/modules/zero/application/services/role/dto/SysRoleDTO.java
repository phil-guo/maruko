package com.act.modules.zero.application.services.role.dto;

import lombok.Data;

import java.time.LocalDateTime;

@Data
public class SysRoleDTO{

    private Long id;
    private LocalDateTime createTime;
    private Integer isDelete;

    /*名称*/
    private String name;

    /*备注*/
    private String remark;
}
