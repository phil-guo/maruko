package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

@Data
@TableName("dc_page")
@SuppressWarnings("all")
public class Page extends BaseEntity<Long> {
    @TableField("name")
    private String name;
    @TableField("`key`")
    private String key;
}
