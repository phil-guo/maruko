package com.act.core.application;

import com.act.core.domain.BaseEntity;

public interface ICurdAppService<TEntity extends BaseEntity<Long>, TEntityDto extends EntityDto> {

    /*
    分页查询
     */
    PagedResultDto PageSearch(PageDto search);

    /*
    添加或删除
     */
    TEntityDto CreateOrEdit(TEntityDto request) throws InstantiationException, IllegalAccessException;

    /**
     * 删除
     *
     * @param id 主键
     */
    void Delete(Long id);
}
