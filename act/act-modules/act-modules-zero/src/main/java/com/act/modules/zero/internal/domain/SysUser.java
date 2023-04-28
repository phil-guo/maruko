package com.act.modules.zero.internal.domain;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;

@Data
@TableName("sys_user")
@SuppressWarnings("all")
public class SysUser extends BaseEntity<Long> {

    //角色Id
    @TableField("roleId")
    private Long roleId;

    //用户名
    @TableField("userName")
    private String userName;

    //密码
    @TableField("password")
    private String password;

    //头像
    @TableField("icon")
    private String icon;
}
