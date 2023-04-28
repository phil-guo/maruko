package com.act.core.domain;

import lombok.Data;

@SuppressWarnings("all")
@Data
public class UserInfoContext {
    private Long userId;
    private String name;
    private String userIcon;
    private Long roleId;
}
