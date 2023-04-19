package com.act.core.application;

import com.act.core.domain.BaseEntity;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.WrapperExtensions;
import com.baomidou.mybatisplus.core.conditions.Wrapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.baomidou.mybatisplus.core.toolkit.Wrappers;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.var;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.lang.reflect.ParameterizedType;
import java.util.ArrayList;
import java.util.List;

/**
 * @author phil.guo
 */
@Service
public class CurdAppService<TEntity extends BaseEntity<Long>, TEntityDto extends EntityDto>
        implements ICurdAppService<TEntity, TEntityDto> {
    @Resource
    private BaseMapper<TEntity> _repos;
    private final Class<TEntity> _entity;
    private final Class<TEntityDto> _dto;

    public CurdAppService() {
        var type = (ParameterizedType) this.getClass().getGenericSuperclass();
        _entity = (Class<TEntity>) type.getActualTypeArguments()[0];
        _dto = (Class<TEntityDto>) type.getActualTypeArguments()[1];
    }

    /*
    分页查询
     */
    public PagedResultDto PageSearch(PageDto search) {
        var page = new Page<TEntity>(search.getPageIndex(), search.getPageSize());

        QueryWrapper<TEntity> queryWrapper = WrapperExtensions.ConvertToWrapper(search.getDynamicFilters());
        var result = _repos.selectPage(page, queryWrapper);
        var datas = BeanUtilsExtensions.copyListProperties(result.getRecords(), () -> {
            try {
                return _dto.newInstance();
            } catch (InstantiationException | IllegalAccessException e) {
                throw new RuntimeException(e);
            }
        });
        return new PagedResultDto(result.getTotal(), datas);
    }

    /*
    添加或者修改
     */
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

    /*
    删除
     */
    public void Delete(Long id) {
        _repos.deleteById(id);
    }

    protected void BeforeCreate(TEntityDto request) {

    }

    protected void BeforeEdit(TEntityDto request) {

    }

    private QueryWrapper<TEntity> ConvertToWrapper(List<DynamicFilter> filters) {
        QueryWrapper<TEntity> queryWrapper = new QueryWrapper<>();
        if (filters.size() == 0)
            return queryWrapper;

        for (var item : filters) {
            if (item.getOperate().equals("Like")) {

            }
        }

        return queryWrapper;
    }
}
