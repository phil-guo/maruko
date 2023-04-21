package com.act.modules.zero.application.services.operate;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.application.services.operate.dto.MenuOfOperateRequest;
import com.act.modules.zero.application.services.operate.dto.MenuOfOperateResponse;
import com.act.modules.zero.application.services.operate.dto.SysOperateDTO;
import com.act.modules.zero.domain.SysOperate;
import com.act.modules.zero.mapper.SysOperateMapper;

public interface SysOperateService extends ICurdAppService<SysOperate, SysOperateDTO, SysOperateMapper> {

    /**
     * 获取菜单功能
     *
     * @param request
     * @return
     */
    MenuOfOperateResponse getMenuOfOperate(MenuOfOperateRequest request);
}
