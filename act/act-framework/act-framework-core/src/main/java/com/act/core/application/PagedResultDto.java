package com.act.core.application;

import lombok.Data;

/*
分页查询返回对象
 */
@Data
public class PagedResultDto {
    private long totalCount;
    private Object datas;

    public PagedResultDto(long total, Object data) {
        totalCount = total;
        datas = data;
    }
}
