package com.act.modules.zero.application.services.role;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.application.services.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.application.services.user.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysRole;

public interface SysRoleService extends ICurdAppService<SysRole, SysRoleDTO> {

    /**
     * 设置角色权限
     * @param request
     * @return
     */
    Boolean setRolePermission(SetRolePermissionRequest request);
}
