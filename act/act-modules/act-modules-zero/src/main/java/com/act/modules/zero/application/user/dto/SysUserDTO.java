package com.act.modules.zero.application.user.dto;

import com.act.core.domain.BaseEntity;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@SuppressWarnings("all")
public class SysUserDTO extends BaseEntity<Long> {
    private Integer isDelete;

    //角色Id
    private Long roleId;

    //用户名
    private String userName;

    //密码
    private String password;

    //头像
    private String icon;

    private String roleName;
}
