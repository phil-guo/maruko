using System;
using System.Collections.Generic;
using System.Linq;
using Maruko.Core.Application;
using Maruko.Core.Application.Servers.Dto;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IObjectMapper = Maruko.Core.ObjectMapping.IObjectMapper;

namespace Maruko.Zero
{
    public class SysMenuService : CurdAppService<SysMenu, SysMenuDTO>, ISysMenuService
    {
        private readonly IFreeSqlRepository<SysOperate> _operate;
        private readonly IFreeSqlRepository<SysRoleMenu> _roleMenu;
        private readonly ILogger<SysMenuService> _logger;
        private readonly IFreeSqlRepository<SysRole> _role;
        private readonly IFreeSqlRepository<Page> _page;

        public SysMenuService(IObjectMapper objectMapper, IFreeSqlRepository<SysMenu> repository,
            IFreeSqlRepository<SysOperate> operate,
            IFreeSqlRepository<SysRoleMenu> roleMenu, ILogger<SysMenuService> logger, IFreeSqlRepository<SysRole> role,
            IFreeSqlRepository<Page> page)
            : base(objectMapper, repository)
        {
            _operate = operate;
            _roleMenu = roleMenu;
            _logger = logger;
            _role = role;
            _page = page;
        }

        public AjaxResponse<object> GetAllParentMenus()
        {
            var data = Table
                .GetAll()
                .Select<SysMenu>()
                .Where(item => item.ParentId == 99999 && item.IsLeftShow)
                .ToList(_ => new
                {
                    Key = _.Name,
                    Value = _.Id
                });

            return new AjaxResponse<object>(data);
        }

        public List<MenusRoleResponse> GetMenusByRole(MenusRoleRequest request)
        {
            var result = new List<MenusRoleResponse>();
            var tree = GetRoleOfMenus(request.RoleId, true);

            result.Add(new MenusRoleResponse
            {
                Id = 0,
                Icon = "el-icon-platform-eleme",
                Title = "首页",
                Path = "/home",
            });

            tree.ForEach(item =>
            {
                var model = new MenusRoleResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Icon = item.Icon ?? "",
                    Path = item.Path,
                    Key = item.Key ?? ""
                };


                if (item.Children.Count > 0)
                    item.Children.ForEach(child =>
                    {
                        model.Children.Add(new MenusRoleResponse
                        {
                            Id = child.Id,
                            Icon = child.Icon ?? "",
                            Path = child.Path,
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

            tree.ForEach(item => { BuildRoleMenusRecursiveTree(listMenus, item); });
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
                            if (JsonConvert.DeserializeObject<List<long>>(child?.Operates ?? "[]").Contains(op.Id))
                                operateModel.Children.Add(new MenuModel
                                { Id = $"{child.Id}_{op.Id}", Lable = op.Name });
                        });
                    });
                else
                {
                    operates.ForEach(op =>
                    {
                        if (JsonConvert.DeserializeObject<List<long>>(item.Operates).Contains(op.Id))
                            model.Children.Add(new MenuModel { Id = $"{item.Id}_{op.Id}", Lable = op.Name });
                    });
                }

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
                else
                {
                    JsonConvert.DeserializeObject<List<int>>(item.Operates).ForEach(operateId =>
                    {
                        operates.ForEach(op =>
                        {
                            if (op.Id != operateId)
                                return;
                            result.MenuIds.Add($"{item.Id}_{op.Id}");
                        });
                    });
                }
            });

            return result;
        }

        public override SysMenuDTO CreateOrEdit(SysMenuDTO request)
        {
            var menu = ObjectMapper.Map<SysMenu>(request);

            if (menu.Id > 0)
            {
                var oldMenu = Table.FirstOrDefault(item => item.Id == menu.Id);

                if (oldMenu.Name != menu.Name || oldMenu.Key != menu.Key)
                {
                    var page = _page.FirstOrDefault(item => item.Key == oldMenu.Key);
                    _page.GetAll().Update<Page>(page.Id)
                        .Set(item => item.Name, menu.Name)
                        .Set(item => item.Key, menu.Key)
                        .ExecuteAffrows();
                }

                oldMenu.Operates = menu.Operates;
                oldMenu.ParentId = menu.ParentId;
                oldMenu.Name = menu.Name;
                oldMenu.Level = menu.Level;
                oldMenu.Url = menu.Url;
                oldMenu.Icon = menu.Icon;
                oldMenu.Key = menu.Key;
                oldMenu.IsLeftShow = menu.IsLeftShow;
                menu = Repository.Update(oldMenu);
            }
            else
            {
                var lastMenu = Table.GetAll().Select<SysMenu>().OrderByDescending(item => item.Id).First();
                if (lastMenu != null && request.Id == 0)
                    menu.Sort = lastMenu.AddOperateSort();

                menu.CreateTime = DateTime.Now;
                menu = Repository.Insert(menu);

                _page.Insert(new Page()
                {
                    Name = menu.Name,
                    Key = menu.Key
                });
            }

            return ObjectMapper.Map<SysMenuDTO>(menu);
        }

        public override void Delete(long id)
        {
            //删除验证
            var roleMenus = _roleMenu.GetAll().Select<SysRoleMenu>().Where(item => item.MenuId == id).ToList();
            if (roleMenus.Count > 0)
            {
                var roleIds = roleMenus.Select(_ => _.RoleId).ToList();
                var roleNames = _role.GetAll().Select<SysRole>()
                    .Where(item => roleIds.Contains(item.Id))
                    .ToList(item => item.Name);
                throw new Exception($"请先解除角色[{string.Join(",", roleNames)}]权限中的菜单关系，在删除菜单");
            }

            base.Delete(id);
        }


        public override PagedResultDto PageSearch(PageDto search)
        {
            List<SysMenu> topNode;
            var query = Table.GetAll().Select<SysMenu>();

            if (search.DynamicFilters.Any())
            {
              var  queryWhere = query.Where(ConditionToLambda(search));
                topNode = queryWhere.ToList();
            }
            else
            {
                topNode = Table.GetAll().Select<SysMenu>()
                    .Where(item => item.ParentId == 99999)
                    .OrderByDescending(item => item.CreateTime)
                    .ToList();
            }

            var nodes = Table.GetAll().Select<SysMenu>()
                .OrderByDescending(item => item.CreateTime)
                .ToList();

            var topNodes = ObjectMapper.Map<List<SysMenuDTO>>(topNode);
            var nodesDTO = ObjectMapper.Map<List<SysMenuDTO>>(nodes);

            topNodes.ForEach(item => BuildMenusRecursiveTree(nodesDTO, item));

            return new PagedResultDto(topNode.Count, topNodes);
        }


        protected override List<SysMenuDTO> ConvertToEntityDTOs(List<SysMenu> entities)
        {
            var data = ObjectMapper.Map<List<SysMenuDTO>>(entities);

            data.ForEach(item =>
            {
                if (string.IsNullOrEmpty(item.Operates))
                {
                    item.Operates = JsonConvert.SerializeObject(new List<int>());
                }
                else
                {
                    JsonConvert.DeserializeObject<List<int>>(item.Operates).ForEach(operateId =>
                    {
                        var operate = _operate.FirstOrDefault(ope => ope.Id == operateId);
                        if (operate == null) return;
                        item.OperateModels.Add(new OperateModel { Id = operateId, Name = operate.Name });
                    });
                }
            });

            var result = FindTopNode(data);

            return result;
        }

        private List<RoleMenuDTO> GetRoleOfMenus(int roleId, bool? isLeftShow = null)
        {
            var datas = GetRoleMenu(roleId, isLeftShow);

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

            tree.ForEach(item => { BuildRoleMenusRecursiveTree(listMenus, item); });

            return tree;
        }

        private List<SysMenu> GetRoleMenu(int roleId, bool? isLeftShow = null)
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
                SysMenu menu = null;
                if (isLeftShow.HasValue)
                    menu = Table.FirstOrDefault(item => item.Id == rm.MenuId && item.IsLeftShow == isLeftShow);
                else
                {
                    menu = Table.FirstOrDefault(item => item.Id == rm.MenuId);
                }

                if (menu == null)
                    return;
                menu.Operates = rm.Operates;
                menus.Add(menu);
            });
            return menus;
        }

        private void BuildRoleMenusRecursiveTree(List<RoleMenuDTO> list, RoleMenuDTO currentTree)
        {
            list.ForEach(item =>
            {
                if (item.ParentId == currentTree.Id)
                    currentTree.Children.Add(item);
            });
        }

        private List<SysMenuDTO> FindTopNode(List<SysMenuDTO> menus)
        {
            var topNodes = new List<SysMenuDTO>();

            long id = 0;
            SysMenuDTO menu;

            menus.ForEach(item =>
            {
                menu = id == 0
                    ? menus.FirstOrDefault(_ => _.Id == item.ParentId)
                    : menus.FirstOrDefault(_ => _.Id == id);
                if (menu != null)
                {
                    id = menu.Id;
                    return;
                }
                else
                {
                    topNodes.Add(item);
                }
            });
            topNodes.ForEach(item => BuildMenusRecursiveTree(menus, item));

            return topNodes;
        }

        private void BuildMenusRecursiveTree(List<SysMenuDTO> menus, SysMenuDTO topNode)
        {
            menus.ForEach(item =>
            {
                if (item.ParentId == topNode.Id)
                    topNode.Children.Add(item);
            });
        }
    }
}