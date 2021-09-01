using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Newtonsoft.Json;
using IObjectMapper = Maruko.Core.ObjectMapping.IObjectMapper;

namespace Maruko.Zero
{
    public class SysMenuService : CurdAppService<SysMenu, SysMenuDTO>, ISysMenuService
    {
        private readonly IFreeSqlRepository<SysOperate> _operate;
        private readonly IFreeSqlRepository<SysRoleMenu> _roleMenu;

        public SysMenuService(IObjectMapper objectMapper, IFreeSqlRepository<SysMenu> repository,
            IFreeSqlRepository<SysOperate> operate,
            IFreeSqlRepository<SysRoleMenu> roleMenu)
            : base(objectMapper, repository)
        {
            _operate = operate;
            _roleMenu = roleMenu;
        }

        public List<MenusRoleResponse> GetMenusByRole(MenusRoleRequest request)
        {
            var result = new List<MenusRoleResponse>();
            var tree = GetRoleOfMenus(request.RoleId);

            result.Add(new MenusRoleResponse { Id = 0, Icon = "el-icon-platform-eleme", Title = "首页", Path = "/home" });

            tree.ForEach(item =>
            {
                var model = new MenusRoleResponse { Id = item.Id, Title = item.Title, Icon = item.Icon ?? "", Path = item.Path, Key = item.Key ?? "" };

                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        model.Children.Add(new MenusRoleResponse
                        {
                            Id = child.Id,
                            Icon = child.Icon ?? "",
                            Path = child.Path, //+ "?id=" + child.Id,
                            Title = child.Title,
                            Key = child.Key ?? ""
                        });
                    });

                result.Add(model);
            });

            return result;
        }

        public RoleMenuResponse GetMenusSetRole(MenusRoleRequest request)
        {
            var result = new RoleMenuResponse();

            var data = Table.GetAll().Select<SysMenu>().OrderBy(item => item.Id).ToList();
            var listMenus = new List<RoleMenuDTO>();
            data.ForEach(item =>
            {
                listMenus.Add(new RoleMenuDTO
                {
                    ParentId = item.ParentId,
                    Id = item.Id,
                    Title = item.Name,
                    Icon = item.Icon,
                    Path = item.Url,
                    Operates = item.Operates,
                    Children = new List<RoleMenuDTO>()
                });
            });

            var tree = listMenus.Where(item => item.ParentId == 99999).ToList();

            var operates = _operate
                .GetAll().Select<SysOperate>()
                .ToList(item => new
                {
                    item.Id,
                    item.Name,
                    item.Unique
                }).ToList();

            tree.ForEach(item => { BuildMeunsRecursiveTree(listMenus, item); });
            tree.ForEach(item =>
            {
                var model = new MenuModel { Id = $"{item.Id}_0", Lable = item.Title };

                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        var operateModel = new MenuModel { Id = $"{child.Id}_0", Lable = child.Title };
                        model.Children.Add(operateModel);
                        operates.ForEach(op =>
                        {
                            if (JsonConvert.DeserializeObject<List<long>>(child.Operates).Contains(op.Id))
                                operateModel.Children.Add(new MenuModel { Id = $"{child.Id}_{op.Id}", Lable = op.Name });
                        });
                    });
                result.List.Add(model);
            });
            var roleMenus = GetRoleOfMenus(request.RoleId);
            roleMenus.ForEach(item =>
            {
                result.MenuIds.Add($"{item.Id}_0");
                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        result.MenuIds.Add($"{child.Id}_0");
                        JsonConvert.DeserializeObject<List<int>>(child.Operates).ForEach(operateId =>
                        {
                            operates.ForEach(op =>
                            {
                                if (op.Id != operateId)
                                    return;
                                result.MenuIds.Add($"{child.Id}_{op.Id}");
                            });
                        });
                    });
            });

            return result;
        }

        public override SysMenuDTO CreateOrEdit(SysMenuDTO request)
        {
            var menu = ObjectMapper.Map<SysMenu>(request);

            if (menu.Id > 0)
            {
                var oldMenu = Table.FirstOrDefault(item => item.Id == menu.Id);
                oldMenu.Operates = menu.Operates;
                oldMenu.ParentId = menu.ParentId;
                oldMenu.Name = menu.Name;
                oldMenu.Level = menu.Level;
                oldMenu.Url = menu.Url;
                oldMenu.Icon = menu.Icon;
                oldMenu.Key = menu.Key;
                menu = Repository.Update(oldMenu);
            }
            else
            {
                var lastMenu = Table.GetAll().Select<SysMenu>().OrderByDescending(item => item.Id).First();
                if (lastMenu != null && request.Id == 0)
                    menu.Sort = lastMenu.AddOperateSort();

                menu.CreateTime = DateTime.Now;
                menu = Repository.Insert(menu);
            }

            return ObjectMapper.Map<SysMenuDTO>(menu);
        }

        public override void Delete(long id)
        {
            //删除验证
            if (_roleMenu.GetAll().Select<SysRoleMenu>().Any(item => item.MenuId == id))
                throw new Exception("请先解除角色权限中的菜单关系，在删除菜单");

            base.Delete(id);
        }

        protected override List<SysMenuDTO> ConvertToEntityDTOs(List<SysMenu> entities)
        {
            var data = ObjectMapper.Map<List<SysMenuDTO>>(entities);

            data.ForEach(item =>
            {
                JsonConvert.DeserializeObject<List<int>>(item.Operates).ForEach(operateId =>
                {
                    var operate = _operate.FirstOrDefault(ope => ope.Id == operateId);
                    if (operate == null) return;
                    item.OperateModels.Add(new OperateModel { Id = operateId, Name = operate.Name });
                });
            });
            return data;
        }

        private List<RoleMenuDTO> GetRoleOfMenus(int roleId)
        {
            var datas = GetRoleMenu(roleId);

            var listMenus = new List<RoleMenuDTO>();
            datas.ForEach(item =>
            {
                listMenus.Add(new RoleMenuDTO
                {
                    ParentId = item.ParentId,
                    Id = item.Id,
                    Title = item.Name,
                    Icon = item.Icon,
                    Path = item.Url,
                    Operates = item.Operates,
                    Key = item.Key ?? ""
                });
            });

            var tree = listMenus.Where(item => item.ParentId == 99999).ToList();

            tree.ForEach(item => { BuildMeunsRecursiveTree(listMenus, item); });

            return tree;
        }

        private List<SysMenu> GetRoleMenu(int roleId)
        {
            var menus = new List<SysMenu>();
            var roleMenusByRole = _roleMenu.GetAll().Select<SysRoleMenu>()
                .Where(item => item.RoleId == roleId)
                .OrderBy(item => item.MenuId)
                .ToList();

            if (roleMenusByRole.Count == 0)
                return menus;
            roleMenusByRole.ForEach(rm =>
            {
                var menu = Table.FirstOrDefault(item => item.Id == rm.MenuId);
                menu.Operates = rm.Operates;
                menus.Add(menu);
            });
            return menus;
        }

        private void BuildMeunsRecursiveTree(List<RoleMenuDTO> list, RoleMenuDTO currentTree)
        {
            list.ForEach(item =>
            {
                if (item.ParentId == currentTree.Id)
                    currentTree.Children.Add(item);
            });
        }


    }
}