package com.act.modules.zero.internal.application.role;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.modules.zero.internal.application.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.internal.application.role.dto.SysRoleDTO;
import com.act.modules.zero.internal.domain.SysRole;
import com.act.modules.zero.internal.mapper.SysRoleMapper;

@SuppressWarnings("all")
public interface SysRoleService extends ICurdAppService<SysRole, SysRoleDTO, SysRoleMapper> {

    /**
     * 获取全部角色
     * @return
     */
    AjaxResponse<Object> getAllRoles();

    /**
     * 设置角色权限
     *
     * @param request
     * @return
     */
    Boolean setRolePermission(SetRolePermissionRequest request);
}
