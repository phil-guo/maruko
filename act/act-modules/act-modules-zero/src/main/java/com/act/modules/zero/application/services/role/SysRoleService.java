package com.act.modules.zero.application.services.role;

import com.act.core.application.ICurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.modules.zero.application.services.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.application.services.role.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysRole;
import com.act.modules.zero.mapper.SysRoleMapper;

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
