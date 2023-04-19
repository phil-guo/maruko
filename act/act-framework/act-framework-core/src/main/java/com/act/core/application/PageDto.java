package com.act.core.application;

import lombok.Data;

import java.sql.Wrapper;
import java.util.List;

@Data
public class PageDto {
    private List<DynamicFilter> dynamicFilters;
    private Integer pageIndex;
    private Integer pageSize;
}

