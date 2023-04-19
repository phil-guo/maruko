package com.act.core.utils;

import com.act.core.application.DynamicFilter;
import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import lombok.var;

import java.util.List;

public class WrapperExtensions {
    public static  <T extends BaseEntity<Long>> QueryWrapper<T> ConvertToWrapper(List<DynamicFilter> filters) {
        QueryWrapper<T> queryWrapper = new QueryWrapper<>();
        if (filters.size() == 0)
            return queryWrapper;

        for (var item : filters) {
            if (item.getOperate().equals("Like")) {
                queryWrapper.like(item.getField(),item.getValue());
            }
            if(item.getOperate().equals("Equal")){
                queryWrapper.eq(item.getField(),item.getValue());
            }
            if(item.getOperate().equals("NotEqual")){
                queryWrapper.ne(item.getField(),item.getValue());
            }
            if(item.getOperate().equals("GreaterThan")){
                queryWrapper.gt(item.getField(),item.getValue());
            }
            if(item.getOperate().equals("GreaterThanOrEqual")){
                queryWrapper.ge(item.getField(),item.getValue());
            }
            if(item.getOperate().equals("LessThan")){
                queryWrapper.lt(item.getField(),item.getValue());
            }
            if(item.getOperate().equals("LessThanOrEqual")){
                queryWrapper.le(item.getField(),item.getValue());
            }
        }
        return queryWrapper;
    }
}
