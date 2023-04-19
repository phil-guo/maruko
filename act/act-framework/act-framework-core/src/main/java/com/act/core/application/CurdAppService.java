package com.act.core.application;

import com.act.core.domain.BaseEntity;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.toolkit.Wrappers;
import lombok.var;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.lang.reflect.ParameterizedType;

/**
 * @author phil.guo
 */
@Service
public class CurdAppService<TEntity extends BaseEntity<Long>, TEntityDto extends EntityDto>
        implements ICurdAppService<TEntity, TEntityDto> {
    @Resource
    private BaseMapper<TEntity> _repos;

    private Class<TEntity> _entity;

    public CurdAppService() {
        var type = (ParameterizedType) this.getClass().getGenericSuperclass();
        _entity = (Class<TEntity>) type.getActualTypeArguments()[0];
    }

    public TEntityDto CreateOrEdit(TEntityDto request) throws InstantiationException, IllegalAccessException {

        TEntity entity = null;

        if (request.getId() == null) {
            entity = _entity.newInstance();
            BeanUtils.copyProperties(request, entity);
            BeforeCreate(request);
            _repos.insert(entity);
        } else {
            BeforeEdit(request);
            var oldEntity = _repos.selectById(request.getId());
            if (oldEntity == null)
                return null;
            BeanUtils.copyProperties(request, oldEntity);
            QueryWrapper<TEntity> wrapper = Wrappers.query();
            wrapper.eq("id", oldEntity.getId());
            _repos.update(oldEntity, wrapper);
            entity = oldEntity;
        }

        var returnDto = request.getClass().newInstance();
        BeanUtils.copyProperties(entity, returnDto);
        return entity == null ? null : (TEntityDto) returnDto;
    }

    protected void BeforeCreate(TEntityDto request) {

    }

    protected void BeforeEdit(TEntityDto request) {

    }
}
