package com.act.modules.zero.internal.controllers;

import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.pageConfig.dto.PageConfigDTO;
import com.act.modules.zero.internal.domain.PageConfig;
import com.act.modules.zero.internal.mapper.PageConfigMapper;
import io.swagger.annotations.Api;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Api(tags = "页面配置")
@RestController
@RequestMapping("/api/v1/pageConfigs/")
public class PageConfigController extends BaseController<PageConfig, PageConfigDTO, PageConfigMapper> {
}
