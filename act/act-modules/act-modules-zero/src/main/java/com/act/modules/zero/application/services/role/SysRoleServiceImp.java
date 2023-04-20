package com.act.modules.zero.application.services.role;

import com.act.core.application.CurdAppService;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.application.services.role.dto.SetRolePermissionRequest;
import com.act.modules.zero.application.services.user.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysRole;
import com.act.modules.zero.mapper.SysRoleMapper;
import org.springframework.stereotype.Service;

@Service
public class SysRoleServiceImp extends CurdAppService<SysRole, SysRoleDTO, SysRoleMapper> implements SysRoleService {

    public Boolean setRolePermission(SetRolePermissionRequest request) {
        return false;
    }

    @Override
    public void delete(Long id) throws FriendlyException {
        if (id == 1)
            throw new FriendlyException("超级管理员不允许被删除！");
        super.delete(id);
    }
}
