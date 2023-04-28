package com.act.modules.zero.internal.application.operate.dto;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@SuppressWarnings("all")
public class SysOperateDTO extends BaseEntity<Long> {
    private String name;
    private int unique;
    private Boolean isBasicData;
    private String remark;
}
