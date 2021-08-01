using System;
using System.Collections.Generic;
using System.Linq;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Newtonsoft.Json;
using IObjectMapper = Maruko.Core.ObjectMapping.IObjectMapper;

namespace Maruko.Zero
{
    public class RoleService : CurdAppService<SysRole, RoleDTO>, IRoleService
    {
        private readonly IFreeSqlRepository<SysMenu> _menu;
        private readonly IFreeSqlRepository<SysRoleMenu> _roleMenu;


        public bool SetRolePermission(SetRolePermissionRequest request)
        {
            var data = _roleMenu.GetAllList(item => item.RoleId == request.RoleId);
            if (data.Count > 0)
                _roleMenu.Delete(item => item.RoleId == request.RoleId);

            if (request.MenuIds.Count == 0)
                return true;

            var models = new List<RolePermissionDTO>();
            var list = new List<SysRoleMenu>();

            request.MenuIds.ToList().ForEach(item =>
            {
                var model = new RolePermissionDTO();
                var operateArray = item.Split('_');
                if (Convert.ToInt32(operateArray.LastOrDefault()) == 0)
                {
                    if (models.FirstOrDefault(m => m.MenuId == Convert.ToInt32(operateArray.FirstOrDefault())) != null)
                        return;
                    model.MenuId = Convert.ToInt32(operateArray.FirstOrDefault());
                    models.Add(model);
                }
                else
                {
                    var data = models.FirstOrDefault(m => m.MenuId == Convert.ToInt32(operateArray.FirstOrDefault()));
                    if (data == null)
                    {
                        model.MenuId = Convert.ToInt32(operateArray.FirstOrDefault());
                        model.Operates.Add(Convert.ToInt32(operateArray.LastOrDefault()));
                        models.Add(model);
                    }
                    else
                    {
                        data.Operates.Add(Convert.ToInt32(operateArray.LastOrDefault()));
                    }
                }
            });

            models.ForEach(rp =>
            {
                var menu = _menu.FirstOrDefault(item => item.Id == rp.MenuId);
                if (menu == null)
                    return;

                var roleMenu = new SysRoleMenu
                {
                    MenuId = rp.MenuId,
                    RoleId = request.RoleId,
                    Operates = JsonConvert.SerializeObject(menu.ParentId == 0
                        ? new List<int>()
                        : rp.Operates)
                };

                list.Add(roleMenu);
            });

            return _roleMenu.BatchInsert(list);
        }

        ///// <summary>
        ///// 查询条件过滤
        ///// </summary>
        ///// <param name="search"></param>
        ///// <returns></returns>
        //protected override Expression<Func<SysRole, bool>> SearchFilter(SearchRoleRequest search)
        //{
        //    Expression<Func<SysRole, bool>> expression = item => true;

        //    if (!string.IsNullOrEmpty(search.Name))
        //        expression = expression.And(item => item.Name.Contains(search.Name));

        //    return expression;
        //}

        public RoleService(IObjectMapper objectMapper, IFreeSqlRepository<SysRole> repository, IFreeSqlRepository<SysMenu> menu, IFreeSqlRepository<SysRoleMenu> roleMenu) : base(objectMapper, repository)
        {
            _menu = menu;
            _roleMenu = roleMenu;
        }
    }
}