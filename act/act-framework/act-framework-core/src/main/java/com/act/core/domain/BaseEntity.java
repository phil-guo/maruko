package com.act.core.domain;

import com.baomidou.mybatisplus.annotation.FieldFill;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;

import java.io.Serializable;
import java.util.Date;


/**
 * @author phil.guo
 */
public abstract class BaseEntity<T> implements Serializable {
    @TableId(type = IdType.AUTO)
    private T id;
    @TableField(value = "createTime",fill = FieldFill.INSERT)
    private Date createTime;
    @TableField("isDelete")
    private int isDelete;
}
