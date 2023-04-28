package com.act.modules.zero.internal.application.role;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.internal.application.role.dto.SysRoleMenuDTO;
import com.act.modules.zero.internal.domain.SysRoleMenu;
import com.act.modules.zero.internal.mapper.SysRoleMenuMapper;
import org.springframework.stereotype.Service;

@Service
@SuppressWarnings("all")
public class SysRoleMenuServiceImp extends CurdAppService<SysRoleMenu, SysRoleMenuDTO, SysRoleMenuMapper> implements SysRoleMenuService {
}
