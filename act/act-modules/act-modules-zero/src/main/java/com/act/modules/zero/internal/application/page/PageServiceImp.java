package com.act.modules.zero.internal.application.page;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.mapper.PageMapper;
import org.springframework.stereotype.Service;

@Service
@SuppressWarnings("all")
public class PageServiceImp extends CurdAppService<Page, PageDTO, PageMapper> implements PageService {
}
