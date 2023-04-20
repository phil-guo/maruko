package com.act.core.application;

import com.act.core.domain.BaseEntity;
import com.act.core.utils.FriendlyException;
import com.github.yulichang.base.MPJBaseMapper;

public interface ICurdAppService<TEntity extends BaseEntity<Long>, TEntityDto> {


    /*
    分页查询
     */
    PagedResultDto pageSearch(PageDto search);

    /*
    添加或删除
     */
    TEntityDto createOrEdit(TEntityDto request) throws InstantiationException, IllegalAccessException, FriendlyException;

    /**
     * 删除
     *
     * @param id 主键
     */
    void delete(Long id) throws FriendlyException;
}
