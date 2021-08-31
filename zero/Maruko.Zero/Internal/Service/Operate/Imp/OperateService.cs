using System;
using System.Collections.Generic;
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
                data = Repository.Update(data);
            }

            return ObjectMapper.Map<OperateDTO>(data);
        }

        public MenuOfOperateResponse GetMenuOfOperate(MenuOfOperateRequest request)
        {
            var roleMenu =
                _roleMenu.FirstOrDefault(item => item.RoleId == request.RoleId && item.MenuId == request.MenuId);
            var idNos = new MenuOfOperateResponse();
            JsonConvert.DeserializeObject<List<int>>(roleMenu?.Operates).ForEach(id =>
            {
                var operate = Repository.FirstOrDefault(item => item.Id == id);
                idNos.Datas.Add(operate?.Unique.ToString());
            });
            return idNos;
        }

        public GetMenuOfOperateByRoleResponse GetMenuOfOperateByRole(GetMenuOfOperateByRoleRequest request)
        {
            var menu = _menu.FirstOrDefault(item => item.Key == request.Key);
            if (menu == null)
                throw new Exception($"key{request.Key}没有对应的菜单！");
            var roleMenu =
                _roleMenu.FirstOrDefault(item => item.RoleId == request.RoleId && item.MenuId == menu.Id);
            var result = new GetMenuOfOperateByRoleResponse();
            JsonConvert.DeserializeObject<List<int>>(roleMenu?.Operates).ForEach(id =>
            {
                var operate = Repository.FirstOrDefault(item => item.Id == id);
                result.Datas.Add(new
                {
                    Unique = operate?.Unique.ToString(),
                    operate?.Name
                });
            });
            return result;
        }

        public OperateService(IObjectMapper objectMapper, IFreeSqlRepository<SysOperate> repository, IFreeSqlRepository<SysRoleMenu> roleMenu, IFreeSqlRepository<SysMenu> menu) : base(objectMapper, repository)
        {
            _roleMenu = roleMenu;
            _menu = menu;
        }
    }
}