package com.act.modules.zero.application.services.menu;

import com.act.core.application.ICurdAppService;
import com.act.modules.zero.application.services.menu.dto.SysMenuDTO;
import com.act.modules.zero.application.services.role.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysMenu;
import com.act.modules.zero.domain.SysRoleMenu;
import com.act.modules.zero.mapper.SysMenuMapper;
import com.act.modules.zero.mapper.SysRoleMenuMapper;

public interface SysMenuService  extends ICurdAppService<SysMenu, SysMenuDTO, SysMenuMapper> {
}
