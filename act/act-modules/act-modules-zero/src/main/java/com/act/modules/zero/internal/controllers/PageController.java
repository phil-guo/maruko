package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.page.PageService;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.mapper.PageMapper;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Api(tags = "页面")
@RestController
@RequestMapping("/api/v1/pages/")
@SuppressWarnings("all")
public class PageController extends BaseController<Page, PageDTO, PageMapper> {

    @Autowired
    private PageService _page;

    @ApiOperation(value = "获取页面详情")
    @GetMapping("getPageDetail")
    public AjaxResponse<Object> getPageDetail(String key) {
        return new AjaxResponse<Object>(_page.getPageDetail(key));
    }
}
