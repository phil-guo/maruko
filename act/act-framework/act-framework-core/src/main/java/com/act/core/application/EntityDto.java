package com.act.core.application;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.*;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.time.LocalDateTime;

@Data
public abstract class EntityDto {
    private Long id;
    private LocalDateTime createTime;
    private Integer isDelete;
}
