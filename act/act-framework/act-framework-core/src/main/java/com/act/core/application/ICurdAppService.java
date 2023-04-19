package com.act.core.application;

import com.act.core.domain.BaseEntity;

public interface ICurdAppService <TEntity extends BaseEntity<Long>, TEntityDto extends EntityDto>{
    TEntityDto CreateOrEdit(TEntityDto request) throws InstantiationException, IllegalAccessException;
}
