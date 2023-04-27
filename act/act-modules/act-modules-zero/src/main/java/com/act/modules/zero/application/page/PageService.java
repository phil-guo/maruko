package com.act.modules.zero.application.page;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.application.page.dto.PageDTO;
import com.act.modules.zero.domain.Page;
import com.act.modules.zero.mapper.PageMapper;

@SuppressWarnings("all")
public interface PageService extends ICurdAppService<Page, PageDTO, PageMapper> {
}
