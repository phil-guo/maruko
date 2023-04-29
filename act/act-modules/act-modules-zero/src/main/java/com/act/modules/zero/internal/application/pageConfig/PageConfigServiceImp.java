package com.act.modules.zero.internal.application.pageConfig;

import com.act.core.application.CurdAppService;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.internal.application.page.PageService;
import com.act.modules.zero.internal.application.pageConfig.dto.PageConfigDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.domain.PageConfig;
import com.act.modules.zero.internal.mapper.PageConfigMapper;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import lombok.var;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@SuppressWarnings("all")
@Service
public class PageConfigServiceImp extends CurdAppService<PageConfig, PageConfigDTO, PageConfigMapper> implements PageConfigService {

    @Autowired
    private PageService _page;

    @Override
    public PageConfigDTO createOrEdit(PageConfigDTO request) throws InstantiationException, IllegalAccessException, FriendlyException {

        var page = _page.getOne(new LambdaQueryWrapper<Page>()
                .eq(Page::getKey, request.getKey()));
        request.setPageId(page.getId());
        var pageConfigDto = super.createOrEdit(request);
        return pageConfigDto;
    }
}
