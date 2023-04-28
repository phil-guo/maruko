package com.act.modules.zero.internal.application.page;

import com.act.core.application.CurdAppService;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.internal.application.page.dto.GetPageDetailDTO;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.application.pageConfig.PageConfigService;
import com.act.modules.zero.internal.application.pageConfig.dto.PageConfigDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.domain.PageConfig;
import com.act.modules.zero.internal.mapper.PageMapper;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import lombok.var;
import org.checkerframework.checker.units.qual.A;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
@SuppressWarnings("all")
public class PageServiceImp extends CurdAppService<Page, PageDTO, PageMapper> implements PageService {

    @Autowired
    private PageConfigService _pageConfig;

    public GetPageDetailDTO getPageDetail(String key) {

        var page = getOne(new LambdaQueryWrapper<Page>()
                .eq(Page::getKey, key));

        if (page == null)
            throw new FriendlyException("page不存在!");

        var pageConfig = _pageConfig.getOne(new LambdaQueryWrapper<PageConfig>()
                .eq(PageConfig::getPageId, page.getId()));

        var pageConfigDTO = new PageConfigDTO();
        BeanUtilsExtensions.copyProperties(pageConfig, pageConfigDTO);
        if (pageConfigDTO != null)
            pageConfigDTO.setKey(page.getKey());

        var result = new GetPageDetailDTO();
        result.setPageConfigs(pageConfigDTO);
        return result;
    }

    @Override
    protected void beforeCreate(PageDTO request) {
        validate(request);
    }

    @Override
    protected void beforeEdit(PageDTO request) {
        validate(request);
    }

    private void validate(PageDTO request) {
        if (StringUtils.isEmpty(request.getKey()))
            throw new FriendlyException("页面标识不能为空");

        if (StringUtils.isEmpty(request.getName()))
            throw new FriendlyException("页面名称不能为空");

        var page = getOne(new LambdaQueryWrapper<Page>()
                .eq(Page::getKey, request.getKey()));

        if (page != null && request.getId() == 0)
            throw new FriendlyException("已经存在key:" + request.getKey() + ",请重新填写");
    }
}
