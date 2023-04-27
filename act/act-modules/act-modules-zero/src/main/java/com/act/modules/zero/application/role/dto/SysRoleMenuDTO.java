package com.act.modules.zero.application.role.dto;

import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
public class SysRoleMenuDTO {

    private Long id;

    //角色Id
    private Long roleId;

    //菜单Id
    private Long menuId;

    //操作
    private String operates;
}
