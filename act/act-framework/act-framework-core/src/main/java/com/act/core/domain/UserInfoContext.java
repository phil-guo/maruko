package com.act.core.domain;

import lombok.Data;

@SuppressWarnings("all")
@Data
public class UserInfoContext {

    public static final String userNamePrex = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    public static final String userIdPrex = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";
    public static final String roleIdPrex = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    public static final String userIconPrex = "http://schemas.microsoft.com/ws/2008/06/identity/claims/icon";


    private Long userId;
    private String name;
    private String userIcon;
    private Long roleId;
}
