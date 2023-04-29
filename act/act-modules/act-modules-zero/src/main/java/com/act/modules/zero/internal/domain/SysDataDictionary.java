package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

@Data
@TableName("sys_dataDictionary")
@SuppressWarnings("all")
public class SysDataDictionary extends BaseEntity<Long> {
    @TableField("`key`")
    private String key;
    @TableField("`value`")
    private String value;
    @TableField("`group`")
    private String group;
    @TableField("isBasicData")
    private Boolean isBasicData = false;
}
