package com.act.modules.zero.application.services.user.imp;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.application.services.user.SysRoleService;
import com.act.modules.zero.application.services.user.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysRole;
import com.act.modules.zero.mapper.SysRoleMapper;
import org.springframework.stereotype.Service;

@Service
public class SysRoleServiceImp extends CurdAppService<SysRole, SysRoleDTO, SysRoleMapper> implements SysRoleService {

}
