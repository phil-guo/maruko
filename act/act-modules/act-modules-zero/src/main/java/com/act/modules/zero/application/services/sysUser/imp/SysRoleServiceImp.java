package com.act.modules.zero.application.services.sysUser.imp;

import com.act.core.application.CurdAppService;
import com.act.modules.zero.application.services.sysUser.SysRoleService;
import com.act.modules.zero.application.services.sysUser.dto.SysRoleDTO;
import com.act.modules.zero.domain.SysRole;
import com.act.modules.zero.mapper.SysRoleMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;

@Service
public class SysRoleServiceImp extends CurdAppService<SysRole, SysRoleDTO, SysRoleMapper> implements SysRoleService {

}
