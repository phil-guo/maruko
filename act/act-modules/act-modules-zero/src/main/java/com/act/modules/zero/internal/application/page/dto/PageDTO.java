package com.act.modules.zero.internal.application.page.dto;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@SuppressWarnings("all")
public class PageDTO extends BaseEntity<Long> {
    private String name;
    private String key;
}
