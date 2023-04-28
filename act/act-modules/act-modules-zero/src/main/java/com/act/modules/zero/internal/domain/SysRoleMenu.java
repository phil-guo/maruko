package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

@Data
@TableName("sys_rolemenu")
@SuppressWarnings("all")
public class SysRoleMenu extends BaseEntity<Long> {

    //角色Id
    @TableField("roleId")
    private Long roleId;

    //菜单Id
    @TableField("menuId")
    private Long menuId;

    //操作
    @TableField("operates")
    private String operates;
}
