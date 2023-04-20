package com.act.modules.zero.application.services.sysUser.dto;

import com.act.core.application.EntityDto;
import lombok.Data;

import java.time.LocalDateTime;

@Data
public class SysRoleDTO extends EntityDto {

    private Long id;
    private LocalDateTime createTime;
    private Integer isDelete;

    /*名称*/
    private String name;

    /*备注*/
    private String remark;
}
