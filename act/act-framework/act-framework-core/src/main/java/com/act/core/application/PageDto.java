package com.act.core.application;

import lombok.Data;

import java.sql.Wrapper;
import java.util.ArrayList;
import java.util.List;

@Data
public class PageDto {
    private List<DynamicFilter> dynamicFilters;
    private Integer pageIndex = 1;
    private Integer pageSize = 20;

    public PageDto() {
        dynamicFilters = new ArrayList<>();
    }
}

