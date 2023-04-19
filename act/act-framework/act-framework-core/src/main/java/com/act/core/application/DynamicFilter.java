package com.act.core.application;

import lombok.Data;

@Data
public class DynamicFilter {
    private String field;
    private String operate;
    private Object value;
}
