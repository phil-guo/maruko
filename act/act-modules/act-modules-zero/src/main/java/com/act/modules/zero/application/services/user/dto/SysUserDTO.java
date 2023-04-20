package com.act.modules.zero.application.services.user.dto;

import lombok.Data;

import java.time.LocalDateTime;

@Data
public class SysUserDTO {

    private Long id;
    private LocalDateTime createTime;
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
