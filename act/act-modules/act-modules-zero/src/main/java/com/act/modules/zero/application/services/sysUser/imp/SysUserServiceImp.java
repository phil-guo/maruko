package com.act.modules.zero.application.services.sysUser.imp;

import com.act.core.application.CurdAppService;
import com.act.core.application.DynamicFilter;
import com.act.core.application.PageDto;
import com.act.core.application.PagedResultDto;
import com.act.core.utils.*;
import com.act.modules.zero.application.services.sysUser.SysRoleService;
import com.act.modules.zero.application.services.sysUser.SysUserService;
import com.act.modules.zero.application.services.sysUser.dto.LoginDTO;
import com.act.modules.zero.application.services.sysUser.dto.SysUserDTO;
import com.act.modules.zero.domain.SysRole;
import com.act.modules.zero.domain.SysUser;
import com.act.modules.zero.mapper.SysUserMapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import com.baomidou.mybatisplus.core.toolkit.Wrappers;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;
import org.springframework.beans.BeanUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.util.Hashtable;
import java.util.Optional;

@Service
public class SysUserServiceImp
        extends CurdAppService<SysUser, SysUserDTO, SysUserMapper>
        implements SysUserService {

    /**
     * 分页查询
     *
     * @param search
     * @return
     */
    @Override
    public PagedResultDto pageSearch(PageDto search) {
        var wrapper = WrapperExtensions.<SysUser>ConvertToWrapper(search.getDynamicFilters())
                .selectAll(SysUser.class)
                .selectAs(SysRole::getName, SysUserDTO::getRoleName)
                .innerJoin(SysRole.class, SysRole::getId, SysUser::getRoleId);

        var page = new Page<SysUserDTO>(search.getPageIndex(), search.getPageSize());
        var result = Table().selectJoinPage(page, SysUserDTO.class, wrapper);
        var datas = BeanUtilsExtensions.copyListProperties(result.getRecords(), SysUserDTO::new);

        return new PagedResultDto(result.getTotal(), datas);
    }

    /**
     * 用户添加或者修改
     *
     * @param request
     * @return
     * @throws InstantiationException
     * @throws IllegalAccessException
     * @throws FriendlyException
     */
    @Override
    public SysUserDTO createOrEdit(SysUserDTO request) throws InstantiationException, IllegalAccessException, FriendlyException {

        if (request == null)
            return null;

        if (request.getId() != null && request.getId() == 1 || request.getUserName().equals("admin"))
            throw new FriendlyException("admin管理员不允许被修改");

        SysUser data = new SysUser();
        if (request.getId() != null && request.getId() == 0) {
            request.setPassword(StringExtensions.ToMd5(request.getPassword()));
            BeanUtilsExtensions.copyProperties(request, data);
            Table().insert(data);
        } else {
            var oldEntity = Table().selectById(request.getId());
            if (oldEntity == null)
                throw new FriendlyException("系统用户不存在");

            oldEntity.setUserName(request.getUserName());
            oldEntity.setRoleId(request.getRoleId());

            if (request.getPassword() != null)
                oldEntity.setPassword(StringExtensions.ToMd5(request.getPassword()));

            QueryWrapper<SysUser> wrapper = Wrappers.query();
            wrapper.eq("id", oldEntity.getId());
            Table().update(oldEntity, wrapper);
            data = oldEntity;
        }

        BeanUtils.copyProperties(data, request);
        return request;
    }

    public AjaxResponse<Object> Login(LoginDTO request) throws InstantiationException, IllegalAccessException {
        var map = new Hashtable<String, Object>();
        map.put("userId", 1);
        var token = JWTUtils.getToken(map);
        return new AjaxResponse<Object>(token);
    }

}
