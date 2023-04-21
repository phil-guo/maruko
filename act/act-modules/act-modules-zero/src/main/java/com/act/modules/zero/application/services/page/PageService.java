package com.act.modules.zero.application.services.page;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.application.services.page.dto.PageDTO;
import com.act.modules.zero.domain.Page;
import com.act.modules.zero.mapper.PageMapper;

public interface PageService extends ICurdAppService<Page, PageDTO, PageMapper> {
}
