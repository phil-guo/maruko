package com.act.core.domain;

import com.baomidou.mybatisplus.annotation.*;
import lombok.Data;

import java.io.Serializable;
import java.time.LocalDateTime;
import java.util.Date;


/**
 * @author phil.guo
 */
@Data
public abstract class BaseEntity<T> implements Serializable {
    @TableId(type = IdType.AUTO)
    private T id;
    @TableField(value = "createTime", fill = FieldFill.INSERT)
    private LocalDateTime createTime = LocalDateTime.now();
    @TableField(value = "isDelete", fill = FieldFill.INSERT)
    @TableLogic
    private Integer isDelete = 0;
}
