package com.act.modules.zero.internal.controllers;

import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.mapper.PageMapper;
import io.swagger.annotations.Api;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Api(tags = "页面")
@RestController
@RequestMapping("/api/v1/pages/")
public class PageController extends BaseController<Page, PageDTO, PageMapper> {

}
