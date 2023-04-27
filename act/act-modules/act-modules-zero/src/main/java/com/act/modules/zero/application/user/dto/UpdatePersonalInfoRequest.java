package com.act.modules.zero.application.user.dto;

import lombok.Data;

@Data
public class UpdatePersonalInfoRequest {
    private long userId;
    private String userName;
    private String password;
    private String icon;
}
