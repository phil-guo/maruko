package com.act.modules.zero.internal.application.page;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.mapper.PageMapper;

@SuppressWarnings("all")
public interface PageService extends ICurdAppService<Page, PageDTO, PageMapper> {
}
