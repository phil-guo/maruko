package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

@Data
@TableName("sys_operate")
@SuppressWarnings("all")
public class SysOperate extends BaseEntity<Long> {
    @TableField("name")
    private String name;
    @TableField("`unique`")
    private int unique = 1001;
    @TableField("isBasicData")
    private Boolean isBasicData = false;
    @TableField("remark")
    private String remark;
}
