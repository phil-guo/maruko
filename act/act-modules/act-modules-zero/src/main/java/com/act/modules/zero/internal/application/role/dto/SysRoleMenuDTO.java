package com.act.modules.zero.internal.application.role.dto;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@SuppressWarnings("all")
public class SysRoleMenuDTO extends BaseEntity<Long> {

    //角色Id
    private Long roleId;

    //菜单Id
    private Long menuId;

    //操作
    private String operates;
}
