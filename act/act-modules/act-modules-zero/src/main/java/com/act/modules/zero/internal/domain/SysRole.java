package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

/*角色*/
@Data
@TableName("sys_role")
@SuppressWarnings("all")
public class SysRole extends BaseEntity<Long> {

    /*名称*/
    @TableField("name")
    private String name;

    /*备注*/
    @TableField("remark")
    private String remark;
}
