package com.act.modules.zero.application.services.role;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.application.services.role.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysRoleMenu;
import com.act.modules.zero.mapper.SysRoleMenuMapper;
import org.springframework.stereotype.Service;

@Service
public class SysRoleMenuServiceImp extends CurdAppService<SysRoleMenu, SysRoleDTO, SysRoleMenuMapper> implements SysRoleMenuService {
}
