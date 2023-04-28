package com.act.modules.zero.internal.application.pageConfig.dto;

import com.act.core.domain.BaseEntity;
import lombok.Data;

@Data
@SuppressWarnings("all")
public class PageConfigDTO extends BaseEntity<Long> {
    private long pageId;
    private String key;
    private String dataUrl;
    private Boolean isRow;
    private Boolean isMultiple;
    private int rowWith;
    private String fields;
    private String buttons;
    private String functions;
    private Boolean isTableOperateRow;
    private Boolean isTableCheckRow;
}
