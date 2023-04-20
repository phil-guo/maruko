package com.act.modules.zero.application.services.sysUser;

import com.act.core.application.ICurdAppService;
import com.act.core.application.PageDto;
import com.act.core.application.PagedResultDto;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import com.act.modules.zero.application.services.sysUser.dto.LoginDTO;
import com.act.modules.zero.application.services.sysUser.dto.ResetPasswordRequest;
import com.act.modules.zero.application.services.sysUser.dto.SysUserDTO;
import com.act.modules.zero.application.services.sysUser.dto.UpdatePersonalInfoRequest;
import com.act.modules.zero.domain.SysUser;

public interface SysUserService extends ICurdAppService<SysUser, SysUserDTO> {

    /**
     * 分页查询
     * @param search
     * @return
     */
    PagedResultDto pageSearch(PageDto search);

    /**
     * 登录
     * @param request
     * @return
     * @throws InstantiationException
     * @throws IllegalAccessException
     */
    AjaxResponse<Object> login(LoginDTO request) throws InstantiationException, IllegalAccessException, FriendlyException;

    /**
     * 用户添加或者修改
     * @param request
     * @return
     * @throws InstantiationException
     * @throws IllegalAccessException
     * @throws FriendlyException
     */
    SysUserDTO createOrEdit(SysUserDTO request) throws InstantiationException, IllegalAccessException, FriendlyException;

    /**
     * 重置密码
     * @param request
     * @return
     */
    AjaxResponse<Object> resetPassword(ResetPasswordRequest request) throws FriendlyException;

    /**
     * 修改个人信息
     * @param request
     * @return
     * @throws FriendlyException
     */
    AjaxResponse<Object> updatePersonalInfo(UpdatePersonalInfoRequest request) throws FriendlyException;
}
