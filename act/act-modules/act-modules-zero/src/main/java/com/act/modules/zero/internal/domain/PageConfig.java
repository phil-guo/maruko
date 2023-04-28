package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

@Data
@TableName("dc_page_config")
@SuppressWarnings("all")
public class PageConfig extends BaseEntity<Long> {
    @TableField("pageId")
    private long pageId;
    @TableField("dataUrl")
    private String dataUrl;
    @TableField("isRow")
    private Boolean isRow;
    @TableField("isMultiple")
    private Boolean isMultiple;
    @TableField("rowWith")
    private int rowWith;
    @TableField("fields")
    private String fields;
    @TableField("buttons")
    private String buttons;
    @TableField("functions")
    private String functions;
    @TableField("isTableOperateRow")
    private Boolean isTableOperateRow;
    @TableField("isTableCheckRow")
    private Boolean isTableCheckRow;
}
