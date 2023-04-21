package com.act.modules.zero.application.services.menu;

import com.act.core.application.CurdAppService;
import com.act.core.utils.BeanUtilsExtensions;
import com.act.core.utils.FriendlyException;
import com.act.core.utils.StringExtensions;
import com.act.modules.zero.application.services.menu.dto.SysMenuDTO;
import com.act.modules.zero.application.services.page.PageService;
import com.act.modules.zero.domain.Page;
import com.act.modules.zero.domain.SysMenu;
import com.act.modules.zero.domain.SysOperate;
import com.act.modules.zero.mapper.SysMenuMapper;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.LambdaUpdateWrapper;
import com.baomidou.mybatisplus.core.toolkit.StringUtils;
import lombok.var;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class SysMenuServiceImp extends CurdAppService<SysMenu, SysMenuDTO, SysMenuMapper> implements SysMenuService {

    @Autowired
    private PageService _page;

    @Override
    public SysMenuDTO createOrEdit(SysMenuDTO request) throws InstantiationException, IllegalAccessException, FriendlyException {

        if (request == null)
            return null;

        var data = new SysMenu();
        if (request.getId() > 0) {
            data = Table().selectById(request.getId());
            if (!data.getName().equals(request.getName()) || !data.getKey().equals(request.getKey())) {
                var oldPage = _page.Table().selectOne(new LambdaQueryWrapper<Page>()
                        .eq(Page::getKey, data.getKey()));
                var newPage = new Page();
                newPage.setId(oldPage.getId());
                newPage.setKey(request.getKey());
                newPage.setName(request.getName());
                _page.updateById(newPage);
            } else {
                data.setOperates(request.getOperates());
                data.setParentId(request.getParentId());
                data.setName(request.getName());
                data.setLevel(request.getLevel());
                data.setUrl(request.getUrl());
                data.setIcon(request.getIcon());
                data.setIsLeftShow(request.getIsLeftShow());
                updateById(data);
            }
        } else {
            var lastMenu = Table().selectOne(new LambdaQueryWrapper<SysMenu>()
                    .orderBy(true, false, SysMenu::getId));
            if (lastMenu != null && request.getId() == 0)
                request.setSort(lastMenu.AddOperateSort());

            var key = StringUtils.isEmpty(request.getKey()) ? "" : request.getKey();

            if (_page.Table().exists(new LambdaQueryWrapper<Page>().eq(Page::getKey, key)))
                throw new FriendlyException("已经存在Key：【" + key + "】");

            BeanUtilsExtensions.copyProperties(request, data);
            save(data);
            var page = new Page();
            page.setName(request.getName());
            page.setKey(request.getKey());
            _page.save(page);
        }
        var result = new SysMenuDTO();
        BeanUtilsExtensions.copyProperties(data, result);
        return result;
    }
}
