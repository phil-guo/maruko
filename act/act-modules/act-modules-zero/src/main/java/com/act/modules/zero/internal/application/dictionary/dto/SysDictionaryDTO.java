package com.act.modules.zero.internal.application.dictionary.dto;

import com.act.core.domain.BaseEntity;
import lombok.Data;

@Data
@SuppressWarnings("all")
public class SysDictionaryDTO extends BaseEntity<Long> {
    private String key;
    private String value;
    private String group;
    private Boolean isBasicData;
}
