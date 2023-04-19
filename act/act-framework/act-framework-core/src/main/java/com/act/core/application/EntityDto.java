package com.act.core.application;

import com.act.core.domain.BaseEntity;
import lombok.Data;
import lombok.EqualsAndHashCode;

@EqualsAndHashCode(callSuper = true)
@Data
public class EntityDto extends BaseEntity<Long> {
}
