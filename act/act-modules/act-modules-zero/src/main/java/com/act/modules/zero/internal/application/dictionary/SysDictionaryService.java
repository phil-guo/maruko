package com.act.modules.zero.internal.application.dictionary;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.modules.zero.internal.application.dictionary.dto.SysDictionaryDTO;
import com.act.modules.zero.internal.domain.SysDataDictionary;
import com.act.modules.zero.internal.mapper.SysDictionaryMapper;

@SuppressWarnings("all")
public interface SysDictionaryService extends ICurdAppService<SysDataDictionary, SysDictionaryDTO, SysDictionaryMapper> {

    /**
     * 根据分组获取数据字典
     *
     * @param groupName
     * @return
     */
    AjaxResponse<Object> getDictionaryByGroup(String groupName);
}
