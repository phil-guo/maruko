﻿using System;
using System.Collections.Generic;
using System.Linq;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Newtonsoft.Json;
using IObjectMapper = Maruko.Core.ObjectMapping.IObjectMapper;

namespace Maruko.Zero
{
    public class OperateService : CurdAppService<SysOperate, OperateDTO>, IOperateService
    {
        private readonly IFreeSqlRepository<SysRoleMenu> _roleMenu;
        private readonly IFreeSqlRepository<SysMenu> _menu;


        public override OperateDTO CreateOrEdit(OperateDTO request)
        {
            SysOperate data = null;
            if (request.Id == 0)
            {
                var entity = Table.GetAll().Select<SysOperate>().OrderByDescending(item => item.Unique).First();
                if (entity != null)
                    request.Unique = entity.Unique + 1;
                else
                    request.Unique = 10001;

                request.CreateTime = DateTime.Now;

                data = Repository.Insert(ObjectMapper.Map<SysOperate>(request));
            }
            else
            {
                data = Table.FirstOrDefault(item => item.Id == request.Id);
                data.Name = request.Name;
                data.Remark = request.Remark;
                data.IsBasicData = request.IsBasicData;
                data = Repository.Update(data);
            }

            return ObjectMapper.Map<OperateDTO>(data);
        }

        public MenuOfOperateResponse GetMenuOfOperate(MenuOfOperateRequest request)
        {
            var roleMenu =
                _roleMenu.FirstOrDefault(item => item.RoleId == request.RoleId && item.MenuId == request.MenuId);

            var menu = _menu.FirstOrDefault(roleMenu.MenuId);

            //取交集
            var operates = JsonConvert.DeserializeObject<List<int>>(menu.Operates)
                .Intersect(JsonConvert.DeserializeObject<List<int>>(roleMenu.Operates))
                .ToList();

            var idNos = new MenuOfOperateResponse();
            operates.ForEach(id =>
            {
                var operate = Repository.FirstOrDefault(item => item.Id == id);
                idNos.Datas.Add(operate?.Unique.ToString());
            });
            return idNos;
        }

        public GetMenuOfOperateByRoleResponse GetMenuOfOperateByRole(GetMenuOfOperateByRoleRequest request)
        {
            var result = new GetMenuOfOperateByRoleResponse();
            var menu = _menu.FirstOrDefault(item => item.Key == request.Key);
            if (menu == null)
                throw new Exception($"key{request.Key}没有对应的菜单！");
            var roleMenu =
                _roleMenu.FirstOrDefault(item => item.RoleId == request.RoleId && item.MenuId == menu.Id);
            if (roleMenu == null)
                throw new Exception("你还没有配置该页面的功能权限！");

            JsonConvert.DeserializeObject<List<int>>(roleMenu?.Operates).ForEach(id =>
            {
                var operate = Repository.FirstOrDefault(item => item.Id == id);
                result.Datas.Add(new
                {
                    Unique = operate?.Unique,
                    operate?.Name
                });
            });
            return result;
        }

        public OperateService(IObjectMapper objectMapper, IFreeSqlRepository<SysOperate> repository,
            IFreeSqlRepository<SysRoleMenu> roleMenu, IFreeSqlRepository<SysMenu> menu) : base(objectMapper, repository)
        {
            _roleMenu = roleMenu;
            _menu = menu;
        }
    }
}