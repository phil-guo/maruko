package com.act.modules.zero.application.services.menu;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.application.services.menu.dto.SysMenuDTO;
import com.act.modules.zero.domain.SysMenu;
import com.act.modules.zero.mapper.SysMenuMapper;
import org.springframework.stereotype.Service;

@Service
public class SysMenuServiceImp extends CurdAppService<SysMenu, SysMenuDTO, SysMenuMapper> implements SysMenuService{
}
