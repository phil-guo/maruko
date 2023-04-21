package com.act.modules.zero.application.services.page;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.application.services.page.dto.PageDTO;
import com.act.modules.zero.domain.Page;
import com.act.modules.zero.mapper.PageMapper;
import org.springframework.stereotype.Service;

@Service
public class PageServiceImp extends CurdAppService<Page, PageDTO, PageMapper> implements PageService {
}
