package com.act.modules.zero.application.services.sysUser.dto;

import com.act.core.application.EntityDto;
import lombok.Data;

@Data
public class SysUserDTO extends EntityDto {
    //角色Id
    private Long roleId;

    //用户名
    private String userName;

    //密码
    private String password;

    //头像
    private String icon;
}
