package com.act.modules.zero.internal.application.pageConfig.dto;

import com.act.core.domain.BaseEntity;
import lombok.Data;

@Data
@SuppressWarnings("all")
public class PageConfigDTO extends BaseEntity<Long> {
    private long pageId;
    private String key;
    private String dataUrl;
    private Boolean isRow = false;
    private Boolean isMultiple = false;
    private int rowWith = 0;
    private String fields;
    private String buttons;
    private String functions = "";
    private Boolean isTableOperateRow = false;
    private Boolean isTableCheckRow = false;
}
