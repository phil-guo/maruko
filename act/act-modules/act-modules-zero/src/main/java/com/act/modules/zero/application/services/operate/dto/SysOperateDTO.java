package com.act.modules.zero.application.services.operate.dto;

import com.baomidou.mybatisplus.annotation.TableField;
import lombok.Data;

import java.time.LocalDateTime;

@Data
public class SysOperateDTO {
    private Long id;
    private LocalDateTime createTime;
    private String name;
    private int unique;
    private Boolean isBasicData;
    private String remark;
}
