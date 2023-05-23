package com.act.modules.zero.internal.application.user;

import com.act.core.application.CurdAppService;
import com.act.core.application.PageDto;
import com.act.core.application.PagedResultDto;
import com.act.core.domain.UserInfoContext;
import com.act.core.utils.*;
import com.act.modules.zero.internal.application.user.dto.*;
import com.act.modules.zero.internal.domain.SysRole;
import com.act.modules.zero.internal.domain.SysUser;
import com.act.modules.zero.internal.mapper.SysUserMapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import com.baomidou.mybatisplus.core.toolkit.Wrappers;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import com.github.yulichang.wrapper.MPJLambdaWrapper;
import lombok.var;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Service;

import java.util.Hashtable;

@Service
@SuppressWarnings("all")
public class SysUserServiceImp
        extends CurdAppService<SysUser, SysUserDTO, SysUserMapper>
        implements SysUserService {

    @Override
    public PagedResultDto pageSearch(PageDto search) {

        var wrapper = WrapperExtensions.<SysUser>ConvertToWrapper(search.getDynamicFilters())
                .selectAll(SysUser.class)
                .selectAs(SysRole::getName, SysUserDTO::getRoleName)
                .innerJoin(SysRole.class, SysRole::getId, SysUser::getRoleId);

        var result = Table()
                .selectJoinPage(new Page<>(search.getPageIndex(), search.getPageSize()), SysUserDTO.class, wrapper);

        var datas = BeanUtilsExtensions
                .copyListProperties(result.getRecords(), SysUserDTO::new);

        return new PagedResultDto(result.getTotal(), datas);
    }

    @Override
    public SysUserDTO createOrEdit(SysUserDTO request) throws FriendlyException {

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

            Table().updateById(oldEntity);
            data = oldEntity;
        }

        BeanUtils.copyProperties(data, request);
        return request;
    }

    @Override
    public void delete(Long id) throws FriendlyException {

        var user = Table().selectById(id);
        if (user == null)
            throw new FriendlyException("用户不存在！");

        if (user.getId() == 1 || user.getUserName().equals("admin"))
            throw new FriendlyException("admin管理员不允许被删除");

        super.delete(id);
    }

    public AjaxResponse<Object> login(LoginDTO request) throws FriendlyException {

        if (StringUtils.isEmpty(request.getName()))
            throw new FriendlyException("用户名不能为空");

        if (StringUtils.isEmpty(request.getPassword()))
            throw new FriendlyException("密码不能为空");

        request.setPassword(StringExtensions.ToMd5(request.getPassword()).toUpperCase());

        var user = Table().selectOne(new MPJLambdaWrapper<SysUser>()
                .eq(SysUser::getUserName, request.getName())
                .eq(SysUser::getPassword, request.getPassword()));
        if (user == null)
            throw new FriendlyException("用户名密码错误");

        var map = new Hashtable<String, Object>();
        map.put(UserInfoContext.userIdPrex, user.getId());
        map.put(UserInfoContext.userNamePrex, StringUtils.isEmpty(user.getUserName()) ? "" : user.getUserName());
        map.put(UserInfoContext.roleIdPrex, user.getRoleId());
        map.put(UserInfoContext.userIconPrex, StringUtils.isEmpty(user.getIcon()) ? "" : user.getIcon());
        var token = JWTUtils.getToken(map);

        var response = new LoginResponse();
        response.setAccessToken(token);
        return new AjaxResponse<>(response);
    }

    public AjaxResponse<Object> resetPassword(ResetPasswordRequest request) throws FriendlyException {

        var user = Table().selectById(request.getUserId());
        if (user == null)
            throw new FriendlyException("系统错误,修改密码失败");

        user.setPassword(StringExtensions.ToMd5("123456").toUpperCase());

        Table().updateById(user);

        return new AjaxResponse<>("重置密码成功");
    }

    public AjaxResponse<Object> updatePersonalInfo(UpdatePersonalInfoRequest request) throws FriendlyException {

        var entity = Table().selectById(request.getUserId());
        if (entity == null)
            throw new FriendlyException("用户不存在");

        if (StringUtils.isEmpty(request.getPassword()))
            entity.setPassword(StringExtensions.ToMd5(request.getPassword()).toUpperCase());

        entity.setUserName(request.getUserName());
        entity.setIcon(request.getIcon());

        Table().updateById(entity);

        return new AjaxResponse<>("更新成功");
    }
}
