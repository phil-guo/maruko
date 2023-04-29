package com.act.core.utils;

import com.act.core.application.DynamicFilter;
import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;

import java.util.List;

public class WrapperExtensions {
    public static <T extends BaseEntity<Long>> MPJLambdaWrapper<T> ConvertToWrapper(List<DynamicFilter> filters) {
        MPJLambdaWrapper<T> queryWrapper = new MPJLambdaWrapper<>();
        if (filters.size() == 0)
            return queryWrapper;

        for (var item : filters) {

            var oldVal = (String) item.getValue();
            Object val = new Object();
            if (oldVal.equals("true") || oldVal.equals("false"))
                val = Boolean.parseBoolean(oldVal);

            if (item.getOperate().equals("Like")) {
                queryWrapper.like(item.getField(), val);
            }
            if (item.getOperate().equals("Equal")) {
                queryWrapper.eq(item.getField(), val);
            }
            if (item.getOperate().equals("NotEqual")) {
                queryWrapper.ne(item.getField(), val);
            }
            if (item.getOperate().equals("GreaterThan")) {
                queryWrapper.gt(item.getField(), val);
            }
            if (item.getOperate().equals("GreaterThanOrEqual")) {
                queryWrapper.ge(item.getField(), val);
            }
            if (item.getOperate().equals("LessThan")) {
                queryWrapper.lt(item.getField(), val);
            }
            if (item.getOperate().equals("LessThanOrEqual")) {
                queryWrapper.le(item.getField(), val);
            }
        }
        return queryWrapper;
    }
}
