package com.act.modules.zero.application.services.page.dto;

import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
public class PageDTO {
    private Long id;
    private LocalDateTime createTime;
    private String name;
    private String key;
}
