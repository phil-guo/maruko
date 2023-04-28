package com.act.modules.zero.internal.application.page;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.internal.application.page.dto.GetPageDetailDTO;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.mapper.PageMapper;

@SuppressWarnings("all")
public interface PageService extends ICurdAppService<Page, PageDTO, PageMapper> {

    /**
     * 获取页面明细数据
     *
     * @param key
     * @return
     */
    GetPageDetailDTO getPageDetail(String key);
}
