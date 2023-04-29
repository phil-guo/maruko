package com.act.core.application;

import com.act.core.domain.BaseEntity;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.core.utils.WrapperExtensions;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.Wrappers;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.github.yulichang.base.MPJBaseMapper;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;

import java.lang.reflect.ParameterizedType;
import java.util.List;

/**
 * @author phil.guo
 */
@SuppressWarnings("all")
public abstract class CurdAppService<TEntity extends BaseEntity<Long>, TEntityDto extends BaseEntity<Long>, BP extends MPJBaseMapper<TEntity>>
        extends ServiceImpl<BP, TEntity>
        implements ICurdAppService<TEntity, TEntityDto, BP> {

    @Autowired
    private BP _repos;
    private final Class<TEntity> _entity;
    private final Class<TEntityDto> _dto;

    public CurdAppService() {
        var type = (ParameterizedType) this.getClass().getGenericSuperclass();
        _entity = (Class<TEntity>) type.getActualTypeArguments()[0];
        _dto = (Class<TEntityDto>) type.getActualTypeArguments()[1];
    }

    public BP Table() {
        return _repos;
    }

    /**
     * 分页查询
     *
     * @param search
     * @return
     */
    public PagedResultDto pageSearch(PageDto search) {

        Page<TEntity> page = Page.of(search.getPageIndex(), search.getPageSize());
        MPJLambdaWrapper<TEntity> queryWrapper = WrapperExtensions.ConvertToWrapper(search.getDynamicFilters());
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

    /**
     * 添加或者修改
     *
     * @param request
     * @return
     * @throws InstantiationException
     * @throws IllegalAccessException
     * @throws FriendlyException
     */
    public TEntityDto createOrEdit(TEntityDto request)
            throws InstantiationException, IllegalAccessException, FriendlyException {

        TEntity entity = null;

        if (request.getId() == null || request.getId() == 0) {
            entity = _entity.newInstance();
            BeanUtils.copyProperties(request, entity);
            beforeCreate(request);
            _repos.insert(entity);
        } else {
            beforeEdit(request);
            entity = _repos.selectById(request.getId());
            if (entity == null)
                return null;
            request.setCreateTime(null);
            BeanUtils.copyProperties(request, entity);
            _repos.updateById(entity);
        }

        var returnDto = request.getClass().newInstance();
        BeanUtils.copyProperties(entity, returnDto);
        return entity == null ? null : (TEntityDto) returnDto;
    }

    /**
     * 删除
     *
     * @param id 主键
     * @throws FriendlyException
     */
    public void delete(Long id) throws FriendlyException {
        _repos.deleteById(id);
    }

    protected void beforeCreate(TEntityDto request) {
    }

    protected void beforeEdit(TEntityDto request) {
    }
}
