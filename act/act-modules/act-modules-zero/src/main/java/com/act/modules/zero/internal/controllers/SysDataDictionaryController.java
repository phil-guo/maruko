package com.act.modules.zero.internal.controllers;

import com.act.core.utils.AjaxResponse;
import com.act.core.web.BaseController;
import com.act.modules.zero.internal.application.dictionary.SysDictionaryService;
import com.act.modules.zero.internal.application.dictionary.dto.SysDictionaryDTO;
import com.act.modules.zero.internal.application.page.dto.PageDTO;
import com.act.modules.zero.internal.domain.Page;
import com.act.modules.zero.internal.domain.SysDataDictionary;
import com.act.modules.zero.internal.mapper.PageMapper;
import com.act.modules.zero.internal.mapper.SysDictionaryMapper;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@SuppressWarnings("all")
@Api(tags = "数据字典")
@RestController
@RequestMapping("/api/v1/sysDataDictionaries/")
public class SysDataDictionaryController extends BaseController<SysDataDictionary, SysDictionaryDTO, SysDictionaryMapper> {

    @Autowired
    private SysDictionaryService _dataDictionary;

    @ApiOperation(value = "根据分组获取数据字典")
    @GetMapping("getDictionaryByGroup")
    public AjaxResponse<Object> getDictionaryByGroup(String groupName) {
        return _dataDictionary.getDictionaryByGroup(groupName);
    }
}
