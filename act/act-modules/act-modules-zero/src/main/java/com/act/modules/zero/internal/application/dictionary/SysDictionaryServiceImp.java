package com.act.modules.zero.internal.application.dictionary;

import com.act.core.application.ComBoxInfo;
import com.act.core.application.CurdAppService;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.core.utils.HttpContextUtils;
import com.act.modules.zero.internal.application.dictionary.dto.SysDictionaryDTO;
import com.act.modules.zero.internal.domain.SysDataDictionary;
import com.act.modules.zero.internal.mapper.SysDictionaryMapper;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import lombok.var;
import org.springframework.stereotype.Service;

@Service
@SuppressWarnings("all")
public class SysDictionaryServiceImp extends CurdAppService<SysDataDictionary, SysDictionaryDTO, SysDictionaryMapper>
        implements SysDictionaryService {

    public AjaxResponse<Object> getDictionaryByGroup(String groupName) {

        var data = Table()
                .selectList(new LambdaQueryWrapper<SysDataDictionary>()
                        .eq(SysDataDictionary::getGroup, groupName));

        var result = BeanUtilsExtensions.copyListProperties(data, ComBoxInfo::new);
        return new AjaxResponse<Object>(data);
    }

    @Override
    public void delete(Long id) throws FriendlyException {
        var entity = getById(id);
        var claimsUserInfo = HttpContextUtils.getUserContext();
        if (entity.getIsBasicData() && claimsUserInfo.getUserId() != 1)
            throw new FriendlyException("非超级管理员不能删除基础数据");
        super.delete(id);
    }

    @Override
    protected void beforeEdit(SysDictionaryDTO request) {
        var claimsUserInfo = HttpContextUtils.getUserContext();
        if (request.getIsBasicData() && claimsUserInfo.getUserId() != 1)
            throw new FriendlyException("非超级管理员不能修改基础数据");
    }
}
