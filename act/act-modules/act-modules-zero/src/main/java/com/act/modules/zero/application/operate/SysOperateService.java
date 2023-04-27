package com.act.modules.zero.application.operate;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.application.operate.dto.*;
import com.act.modules.zero.domain.SysOperate;
import com.act.modules.zero.mapper.SysOperateMapper;

@SuppressWarnings("all")
public interface SysOperateService extends ICurdAppService<SysOperate, SysOperateDTO, SysOperateMapper> {

    /**
     * 根据角色获取菜单的操作
     *
     * @param request
     * @return
     * @throws FriendlyException
     */
    GetMenuOfOperateByRoleResponse getMenuOfOperateByRole(GetMenuOfOperateByRoleRequest request) throws FriendlyException;

    /**
     * 获取菜单功能
     *
     * @param request
     * @return
     */
    MenuOfOperateResponse getMenuOfOperate(MenuOfOperateRequest request);
}
